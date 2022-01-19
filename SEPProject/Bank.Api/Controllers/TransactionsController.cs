using Bank.Api.DTOs;
using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
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
using TransactionStatus = Bank.Core.Model.TransactionStatus;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentCardService _paymentCardService;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        private IHttpClientFactory _httpClientFactory;
        private readonly IPSPResponseRepository _PSPResponseRepository;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransactionsController(ITransactionRepository transactionRepository, IPaymentCardService paymentCardService,
            ITransactionService transactionService, IAccountService accountService, IHttpClientFactory httpClientFactory,
            IPSPResponseRepository pSPResponseRepository, ILogger<TransactionsController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _transactionRepository = transactionRepository;
            _paymentCardService = paymentCardService;
            _transactionService = transactionService;
            _accountService = accountService;
            _httpClientFactory = httpClientFactory;
            _PSPResponseRepository = pSPResponseRepository;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpGet("{id}/transaction-status")]
        public IActionResult GetTransactionStatusById(Guid id)
        {
            return Ok(new DTOs.TransactionStatus(_transactionRepository.GetById(id).TransactionStatus.ToString()));
        }

        [HttpPost]
        public IActionResult Create(CardInfo cardInfo)
        {
            Result<Transaction> transactionResult = null;
            Core.Model.PSPRequest request = _PSPResponseRepository.GetByPaymentId(cardInfo.PaymentId).PSPRequest;
            var hiddenPAN = HidePAN(cardInfo.PAN);
            if (_transactionRepository.GetByPaymentId(cardInfo.PaymentId) != null)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, request.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString(), transactionResult.Value.Id));
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", new
                {
                    cardInfo.PaymentId,
                    hiddenPAN,
                    cardInfo.CardHolderName,
                    cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName,
                    cardInfo.Amount
                }, "Transaction with that id already exists");
                return BadRequest(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString(), transactionResult.Value.Id));
            }
            if (!cardInfo.PAN.Substring(0, 6).Equals(Config.BankPan))
            {
                //send to pcc
                transactionResult = _transactionService.Create(cardInfo.Amount, request.Currency, DateTime.Now, cardInfo.PaymentId, cardInfo.PAN,
                TransactionStatus.Pending);
                ForwardToPCC(new PCCRequest(transactionResult.Value.Id, transactionResult.Value.Timestamp, cardInfo.PaymentId, cardInfo.PAN, Config.BankPan,
                    cardInfo.SecurityCode, cardInfo.CardHolderName, cardInfo.ExpirationDate, cardInfo.Amount, cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName, request.Currency));
                _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
                return Created(this.Request.Path + "/" + transactionResult.Value.Id,
                new PSPTransaction(request.MerchantOrderId, TransactionStatus.Pending.ToString(), transactionResult.Value.Id));
            }
            Result result = _paymentCardService.Pay(new Core.Model.PaymentCard(Guid.Empty, cardInfo.PAN, cardInfo.SecurityCode, cardInfo.CardHolderName,
                cardInfo.ExpirationDate, Guid.Empty, ""), cardInfo.Amount, request.Currency, cardInfo.AcquirerAccountNumber);
            if (result.IsFailure && (result.Error.Equals("Amount can not be negative number") ||
                result.Error.Equals("There is not enough resources on this bank account.")))
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, request.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Failed);
                ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Failed.ToString(), transactionResult.Value.Id));
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", new
                {
                    cardInfo.PaymentId,
                    hiddenPAN,
                    cardInfo.CardHolderName,
                    cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName,
                    cardInfo.Amount
                }, transactionResult.Error);
                return BadRequest(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Failed.ToString(), transactionResult.Value.Id));
            }
            else if (result.IsFailure)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, request.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString(), transactionResult.Value.Id));
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", new
                {
                    cardInfo.PaymentId,
                    hiddenPAN,
                    cardInfo.CardHolderName,
                    cardInfo.AcquirerAccountNumber,
                    cardInfo.AcquirerName,
                    cardInfo.Amount
                }, cardInfo, result.Error);
                return BadRequest(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString(), transactionResult.Value.Id));
            }
            transactionResult = _transactionService.Create(cardInfo.Amount, request.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Success);
            _transactionRepository.Edit(transactionResult.Value);
            ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Success.ToString(), transactionResult.Value.Id));
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            return Created(this.Request.Path + "/" + transactionResult.Value.Id,
                new PSPTransaction(request.MerchantOrderId, TransactionStatus.Success.ToString(), transactionResult.Value.Id));
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
                perDiem.Currency);

            if (transactionResult.Value.TransactionStatus == TransactionStatus.Failed)
            {
                _logger.LogError("Failed to create Transaction with Per Diem {@PerDiem}, Error: {@Error}", new
                {
                    perDiem
                }, "Transaction has failed, invalid Unique Personal Registration Number");
                return BadRequest("Transaction has failed, invalid Unique Personal Registration Number");
            }
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            _accountService.UpdateBalance(transactionResult.Value.AcquirerId, transactionResult.Value.Amount, transactionResult.Value.Currency);
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
                    Core.Model.PSPRequest request = _PSPResponseRepository.GetByPaymentId(transaction.PaymentId).PSPRequest;
                    ForwardTransaction(new PSPTransaction(request.MerchantId, TransactionStatus.Success.ToString(), transaction.Id));
                }

                return new NoContentResult();
            }
            return BadRequest(ModelState);
        }

        private async void ForwardTransaction(PSPTransaction transaction)
        {
            var transactionJson = new StringContent(
              JsonSerializer.Serialize(transaction),
              Encoding.UTF8,
              Application.Json);

            var path = $"{_webHostEnvironment.ContentRootPath}\\clientcertbank.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage =
            await client.PutAsync(Config.PSPServerAddress, transactionJson);
            httpResponseMessage.Dispose();
        }

        private async void ForwardToPCC(PCCRequest pccRequest)
        {
            var cardInfoJson = new StringContent(
              JsonSerializer.Serialize(pccRequest),
              Encoding.UTF8,
              Application.Json);

            var path = $"{_webHostEnvironment.ContentRootPath}\\clientcertbank.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage =
            await client.PostAsync(Config.PCCServerAddress, cardInfoJson);
            httpResponseMessage.Dispose();
        }

        private string HidePAN(string pan)
        {
            return pan.Substring(0, 6) + "******" + pan.Substring(pan.Length - 4);
        }
    }
}