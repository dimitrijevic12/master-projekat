using Bank.Api.DTOs;
using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank.Api.Controllers
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
            Core.Model.PSPRequest request = _PSPResponseRepository.GetByPaymentId(cardInfo.PaymentId).PSPRequest;
            if (_transactionRepository.GetByPaymentId(cardInfo.PaymentId) != null) 
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                //ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString()));
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", cardInfo, "Transaction with that id already exists");
                return BadRequest(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString()));
            }
            // TODO
            /*if (!cardInfo.PAN.Substring(0, 5).Equals("123456"))
            {
                send to pcc
            }*/
            Result result = _paymentCardService.Pay(new Core.Model.PaymentCard(Guid.Empty, cardInfo.PAN, cardInfo.SecurityCode, cardInfo.CardHolderName,
                cardInfo.ExpirationDate, Guid.Empty), cardInfo.Amount, cardInfo.AcquirerAccountNumber);
            if (result.IsFailure && (result.Error.Equals("Amount can not be negative number") || 
                result.Error.Equals("There is not enough resources on this bank account."))) 
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, DateTime.Now, cardInfo.PaymentId, 
                    cardInfo.PAN, TransactionStatus.Failed);
                //ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Failed.ToString()));
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", cardInfo, transactionResult.Error);
                return BadRequest(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Failed.ToString()));
            }
            else if (result.IsFailure)
            {
                transactionResult = _transactionService.Create(cardInfo.Amount, DateTime.Now, cardInfo.PaymentId,
                    cardInfo.PAN, TransactionStatus.Error);
                //ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString()));
                _logger.LogError("Failed to create Transaction with Card Info {@CardInfo}, Error: {@Error}", cardInfo, transactionResult.Error);
                return BadRequest(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Error.ToString()));
            }
            transactionResult = _transactionService.Create(cardInfo.Amount, DateTime.Now, cardInfo.PaymentId, cardInfo.PAN,
                TransactionStatus.Success);
            //ForwardTransaction(new PSPTransaction(request.MerchantOrderId, TransactionStatus.Success.ToString()));
            _logger.LogInformation("Created Transaction {@Transaction}", transactionResult.Value);
            return Created(this.Request.Path + "/" + transactionResult.Value.Id,
                new PSPTransaction(request.MerchantOrderId, TransactionStatus.Success.ToString()));
        }

        private async void ForwardTransaction(PSPTransaction transaction)
        {
            var transactionJson = new StringContent(
              JsonSerializer.Serialize(transaction),
              Encoding.UTF8,
              Application.Json);

            HttpClient client = _httpClientFactory.CreateClient();
            using var httpResponseMessage =
            await client.PutAsync("http://localhost:60212/api/transactions/status", transactionJson);
            httpResponseMessage.Dispose();
        }
    }
}
