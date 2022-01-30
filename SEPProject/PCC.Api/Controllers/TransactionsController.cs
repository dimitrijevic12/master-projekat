using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PCC.Api.DTOs;
using PCC.Core.Interface.Repository;
using PCC.Core.Model;
using PCC.Core.Services;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PCC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransactionsController(TransactionService transactionService, ITransactionRepository transactionRepository,
            IHttpClientFactory httpClientFactory, ILogger<TransactionsController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PCCRequest pccRequest)
        {
            Result<Transaction> transactionResult = _transactionService.Create(pccRequest.Amount, pccRequest.Currency, DateTime.Now,
                pccRequest.PaymentId, pccRequest.PAN, pccRequest.AcquirerBankPAN, TransactionStatus.Pending, pccRequest.AcquirerOrderId,
                pccRequest.AcquirerTimestamp, Guid.Empty, new DateTime());
            var hiddenPAN = HidePAN(pccRequest.PAN);
            if (transactionResult.IsFailure)
            {
                _logger.LogError("Failed to create Transaction with PCC Request {@PCCRequest}, Error: {@Error}", new
                {
                    pccRequest.AcquirerOrderId,
                    pccRequest.AcquirerTimestamp,
                    pccRequest.PaymentId,
                    hiddenPAN,
                    pccRequest.AcquirerBankPAN,
                    pccRequest.CardHolderName,
                    pccRequest.Amount,
                    pccRequest.Currency,
                    pccRequest.AcquirerAccountNumber,
                    pccRequest.AcquirerName
                }, transactionResult.Error);
                return BadRequest(transactionResult.Error);
            }
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            await ForwardCardInfo(transactionResult.Value, pccRequest.AcquirerOrderId,
                new CardInfo(pccRequest.AcquirerOrderId, pccRequest.AcquirerTimestamp, pccRequest.PaymentId, pccRequest.PAN, pccRequest.SecurityCode,
                pccRequest.CardHolderName, pccRequest.ExpirationDate, pccRequest.Amount, pccRequest.AcquirerAccountNumber, pccRequest.AcquirerName,
                pccRequest.Currency));
            return Created($"{this.Request.Path}/{transactionResult.Value.Id}", pccRequest);
        }

        [HttpPost("per-diem")]
        public async Task<IActionResult> CreatePerDiem(PCCPerDiemRequest pccPerDiemRequest)
        {
            Result<Transaction> transactionResult = _transactionService.CreatePerDiem(pccPerDiemRequest.Amount, pccPerDiemRequest.Currency, DateTime.Now,
                pccPerDiemRequest.PaymentId, "", "", TransactionStatus.Pending,
                Guid.Empty, pccPerDiemRequest.AcquirerTimestamp, Guid.Empty, new DateTime());
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            await ForwardPerDiem(transactionResult.Value, pccPerDiemRequest);
            return Created($"{this.Request.Path}/{transactionResult.Value.Id}", pccPerDiemRequest);
        }

        private async Task ForwardCardInfo(Transaction transaction, Guid transactionId, CardInfo cardInfo)
        {
            var cardInfoJson = new StringContent(
              System.Text.Json.JsonSerializer.Serialize(cardInfo),
              Encoding.UTF8,
              Application.Json);
            var path = $"{_webHostEnvironment.ContentRootPath}\\pcc.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage = await client.PostAsync(Config.IssuerBankServerAddress, cardInfoJson);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            PCCResponse pccResponse = JsonConvert.DeserializeObject<PCCResponse>(content);
            Enum.TryParse(pccResponse.TransactionStatus, out TransactionStatus transactionStatus);
            transaction.TransactionStatus = transactionStatus;
            transaction.IssuerOrderId = pccResponse.IssuerOrderId;
            transaction.IssuerTimestamp = pccResponse.IssuerTimestamp;
            _transactionRepository.Edit(transaction);
            ForwardTransactionStatus(transactionId, transactionStatus);
            httpResponseMessage.Dispose();
        }

        private async Task ForwardPerDiem(Transaction transaction, PCCPerDiemRequest pCCPerDiemRequest)
        {
            var perDiemJson = new StringContent(
              System.Text.Json.JsonSerializer.Serialize(new BankPerDiemRequest(transaction.Id, transaction.Timestamp, pCCPerDiemRequest.Amount,
              pCCPerDiemRequest.AcquirerAccountNumber, pCCPerDiemRequest.Currency)),
              Encoding.UTF8,
              Application.Json);
            var path = $"{_webHostEnvironment.ContentRootPath}\\pcc.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage = await client.PostAsync(Config.AcquirerBankServerAddress + "/per-diem-pcc", perDiemJson);
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            PCCResponse pccResponse = JsonConvert.DeserializeObject<PCCResponse>(content);
            Enum.TryParse(pccResponse.TransactionStatus, out TransactionStatus transactionStatus);
            transaction.TransactionStatus = transactionStatus;
            transaction.IssuerOrderId = pccResponse.IssuerOrderId;
            transaction.IssuerTimestamp = pccResponse.IssuerTimestamp;
            _transactionRepository.Edit(transaction);
            ForwardTransactionStatusToIssuerBank(pCCPerDiemRequest.AcquirerTransactionId, transactionStatus);
            httpResponseMessage.Dispose();
        }

        private async void ForwardTransactionStatus(Guid transactionId, TransactionStatus transactionStatus)
        {
            var patchPayloadList = new ArrayList();
            PatchRequestPayload patchRequestPayload = new PatchRequestPayload("replace", "/transactionStatus", (int)transactionStatus);
            patchPayloadList.Add(patchRequestPayload);
            var payload = new StringContent(
              System.Text.Json.JsonSerializer.Serialize(patchPayloadList),
              Encoding.UTF8,
              Application.Json);
            var path = $"{_webHostEnvironment.ContentRootPath}\\pcc.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage = await client.PatchAsync(Config.AcquirerBankServerAddress + $"/{transactionId}", payload);
            httpResponseMessage.Dispose();
        }

        private async void ForwardTransactionStatusToIssuerBank(Guid transactionId, TransactionStatus transactionStatus)
        {
            var patchPayloadList = new ArrayList();
            PatchRequestPayload patchRequestPayload = new PatchRequestPayload("replace", "/transactionStatus", (int)transactionStatus);
            patchPayloadList.Add(patchRequestPayload);
            var payload = new StringContent(
              System.Text.Json.JsonSerializer.Serialize(patchPayloadList),
              Encoding.UTF8,
              Application.Json);
            var path = $"{_webHostEnvironment.ContentRootPath}\\pcc.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage = await client.PatchAsync(Config.IssuerBankServerAddress + $"/{transactionId}", payload);
            httpResponseMessage.Dispose();
        }

        private string HidePAN(string pan)
        {
            return pan.Substring(0, 6) + "******" + pan.Substring(pan.Length - 4);
        }
    }
}