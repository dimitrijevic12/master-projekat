using CSharpFunctionalExtensions;
using IssuerBank.Api.DTOs;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using TransactionStatus = IssuerBank.Core.Model.TransactionStatus;

namespace IssuerBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentCardService _paymentCardService;
        private readonly ITransactionService _transactionService;
        private IHttpClientFactory _httpClientFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;
        private readonly ILogger<MerchantsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransactionsController(ITransactionRepository transactionRepository, IPaymentCardService paymentCardService,
            ITransactionService transactionService, IHttpClientFactory httpClientFactory, IAccountRepository accountRepository,
            IAccountService accountService, ILogger<MerchantsController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _transactionRepository = transactionRepository;
            _paymentCardService = paymentCardService;
            _transactionService = transactionService;
            _httpClientFactory = httpClientFactory;
            _accountRepository = accountRepository;
            _accountService = accountService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CardInfo cardInfo)
        {
            Result<Transaction> transactionResult = null;
            DateTime timestamp = DateTime.Now;
            var hiddenPAN = HidePAN(cardInfo.PAN);
            if (_transactionRepository.GetByPaymentId(cardInfo.PaymentId) != null)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, cardInfo.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", new
                {
                    cardInfo.PaymentId,
                    hiddenPAN,
                    cardInfo.CardHolderName,
                    cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName,
                    cardInfo.Amount
                }, "Transaction with that id already exists");
                return BadRequest(new PCCResponse(TransactionStatus.Error.ToString(), cardInfo.AcquirerOrderId, cardInfo.AcquirerTimestamp,
                    transactionResult.Value.Id, transactionResult.Value.Timestamp));
            }
            Result result = _paymentCardService.Pay(new Core.Model.PaymentCard(Guid.Empty, cardInfo.PAN, cardInfo.SecurityCode, cardInfo.CardHolderName,
                cardInfo.ExpirationDate, Guid.Empty), cardInfo.Amount, cardInfo.Currency, cardInfo.AcquirerAccountNumber);
            if (result.IsFailure && (result.Error.Equals("Amount can not be negative number") ||
                result.Error.Equals("There is not enough resources on this bank account.")))
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, cardInfo.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Failed);
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", new
                {
                    cardInfo.PaymentId,
                    hiddenPAN,
                    cardInfo.CardHolderName,
                    cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName,
                    cardInfo.Amount
                }, transactionResult.Error);
                return BadRequest(new PCCResponse(TransactionStatus.Failed.ToString(), cardInfo.AcquirerOrderId, cardInfo.AcquirerTimestamp,
                    transactionResult.Value.Id, transactionResult.Value.Timestamp));
            }
            else if (result.IsFailure)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, cardInfo.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", new
                {
                    cardInfo.PaymentId,
                    hiddenPAN,
                    cardInfo.CardHolderName,
                    cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName,
                    cardInfo.Amount
                }, cardInfo, result.Error);
                return BadRequest(new PCCResponse(TransactionStatus.Error.ToString(), cardInfo.AcquirerOrderId, cardInfo.AcquirerTimestamp,
                    transactionResult.Value.Id, transactionResult.Value.Timestamp));
            }
            transactionResult = _transactionService.Create(cardInfo.Amount, cardInfo.Currency, DateTime.Now, cardInfo.PaymentId, cardInfo.PAN,
                TransactionStatus.Success);
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            return Created(this.Request.Path + "/" + transactionResult.Value.Id,
                    new PCCResponse(TransactionStatus.Success.ToString(), cardInfo.AcquirerOrderId, cardInfo.AcquirerTimestamp,
                    transactionResult.Value.Id, transactionResult.Value.Timestamp));
        }

        [HttpPost]
        [Route("per-diem")]
        public IActionResult CreatePerDiemTransaction(PerDiem perDiem)
        {
            if (perDiem.Amount < 0)
            {
                _logger.LogError("Failed to create Transaction with Per Diem {@PerDiem}, Error: {@Error}", new
                {
                    perDiem
                }, "Transaction has failed, negative amount");
                return BadRequest("Transaction has failed, negative amount");
            }
            Result<Transaction> transactionResult = null;
            transactionResult = _transactionService.CreatePerDiem(perDiem.UniquePersonalRegistrationNumber, perDiem.Amount,
                perDiem.Currency, perDiem.TransactionId);
            Account issuerAccount = _accountRepository.GetByAccountNumber(perDiem.AccountNumber);
            if (issuerAccount == null)
            {
                //send to pcc
                ForwardToPCC(new PCCPerDiemRequest(transactionResult.Value.Timestamp, transactionResult.Value.Id, transactionResult.Value.PaymentId,
                    transactionResult.Value.Amount, perDiem.AccountNumber, perDiem.Currency));
                _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
                return Created(this.Request.Path + "/" + transactionResult.Value.Id, "");
            }
            if (transactionResult.Value.TransactionStatus == TransactionStatus.Failed)
            {
                _logger.LogError("Failed to create Transaction with Per Diem {@PerDiem}, Error: {@Error}", new
                {
                    perDiem
                }, "Transaction has failed, invalid Unique Personal Registration Number");
                return BadRequest("Transaction has failed, invalid Unique Personal Registration Number");
            }
            if (transactionResult.Value.TransactionStatus == TransactionStatus.Error)
            {
                _logger.LogError("Failed to create Transaction with Per Diem {@PerDiem}, Error: {@Error}", new
                {
                    perDiem
                }, "Transaction has failed, invalid Unique Personal Registration Number");
                return BadRequest("Transaction has failed, invalid Unique Personal Registration Number");
            }
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            return Created(this.Request.Path + "/" + transactionResult.Value.Id, "");
        }

        [HttpPatch("{id}")]
        [Authorize]
        public IActionResult Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<Transaction> patchDoc)
        {
            if (patchDoc != null)
            {
                Transaction transaction = _transactionRepository.GetById(id);
                patchDoc.ApplyTo(transaction, ModelState);
                _transactionRepository.Edit(transaction);
                _logger.LogInformation("Transaction status updated: {@Transaction} with status {@TransactionStatus}", transaction,
                    transaction.TransactionStatus);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (transaction.TransactionStatus == TransactionStatus.Success)
                {
                    _accountService.UpdateBalance(transaction.AcquirerId, transaction.Amount, transaction.Currency);
                    //ForwardPerDiem(new WebShopResponse(transaction.Id, PerdiemStatus.Paid.ToString()));
                    return new NoContentResult();
                }
                //ForwardPerDiem(new WebShopResponse(transaction.Id, PerdiemStatus.ShouldPay.ToString()));
                return new NoContentResult();
            }
            return BadRequest(ModelState);
        }

        private async void ForwardToPCC(PCCPerDiemRequest pccPerDiemRequest)
        {
            var pccPerDiemJson = new StringContent(
              JsonSerializer.Serialize(pccPerDiemRequest),
              Encoding.UTF8,
              Application.Json);

            var path = $"{_webHostEnvironment.ContentRootPath}\\clientcertissuerbank.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage =
            await client.PostAsync(Config.PCCServerAddress, pccPerDiemJson);
            httpResponseMessage.Dispose();
        }

        private async void ForwardPerDiem(WebShopResponse webShopResponse)
        {
            var webShopResponseJson = new StringContent(
              JsonSerializer.Serialize(webShopResponse),
              Encoding.UTF8,
              Application.Json);

            var path = $"{_webHostEnvironment.ContentRootPath}\\clientcertissuerbank.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage =
            await client.PutAsync(Config.WebShopAddress, webShopResponseJson);
            httpResponseMessage.Dispose();
        }

        private string HidePAN(string pan)
        {
            return pan.Substring(0, 6) + "******" + pan.Substring(pan.Length - 4);
        }
    }
}