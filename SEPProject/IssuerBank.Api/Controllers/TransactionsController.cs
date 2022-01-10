using CSharpFunctionalExtensions;
using IssuerBank.Api.DTOs;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

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
        private readonly IPSPResponseRepository _PSPResponseRepository;
        private readonly ILogger<MerchantsController> _logger;

        public TransactionsController(ITransactionRepository transactionRepository, IPaymentCardService paymentCardService,
            ITransactionService transactionService, IHttpClientFactory httpClientFactory,
            IPSPResponseRepository pSPResponseRepository, ILogger<MerchantsController> logger)
        {
            _transactionRepository = transactionRepository;
            _paymentCardService = paymentCardService;
            _transactionService = transactionService;
            _httpClientFactory = httpClientFactory;
            _PSPResponseRepository = pSPResponseRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create(CardInfo cardInfo)
        {
            Result<Transaction> transactionResult = null;
            if (_transactionRepository.GetByPaymentId(cardInfo.PaymentId) != null)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, cardInfo.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", cardInfo, "Transaction with that id already exists");
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
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", cardInfo, transactionResult.Error);
                return BadRequest(new PCCResponse(TransactionStatus.Failed.ToString(), cardInfo.AcquirerOrderId, cardInfo.AcquirerTimestamp,
                    transactionResult.Value.Id, transactionResult.Value.Timestamp));
            }
            else if (result.IsFailure)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, cardInfo.Currency, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", cardInfo, transactionResult.Error);
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
    }
}