using CardPayment.Core.DTOs;
using CardPayment.Core.Interface.Repository;
using CardPayment.Core.Model;
using CardPayment.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

namespace CardPayment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionService _transactionService;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransactionsController(ITransactionRepository transactionRepository, TransactionService transactionService, IHttpClientFactory httpClientFactory,
            ILogger<TransactionsController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{orderId}")]
        public IActionResult GenerateRequestForBank(Guid orderId)
        {
            var request = _transactionService.CreateRequestForBank(orderId);
            if (request == null)
            {
                _logger.LogError("Failed to create request for transaction with order id: {id}", orderId);
                return BadRequest();
            }
            _logger.LogInformation("Created request : {@request}", request);
            return Ok(request);
        }

        [HttpPut("paymentId")]
        public IActionResult SetTransactionsPaymentId(TransactionsPaymentIdDTO transactionsPaymentIdDTO)
        {
            Transaction transaction = _transactionService.SetTransactionsPaymentId(transactionsPaymentIdDTO);
            if (transaction  == null)
            {
                _logger.LogError("Failed to edit payment types for transaction with order id: {id}", transactionsPaymentIdDTO.OrderId);
                return BadRequest();
            }
            _logger.LogInformation("Edited payment id for transaction : {@transaction}", transaction);
            return Ok("Successfully edited payment id.");
        }

        [HttpPut("status")]
        public IActionResult EditTransactionStatus(TransactionStatusDTO transactionStatusDTO)
        {
            Transaction transaction = _transactionService.EditTransaction(transactionStatusDTO);
            if (transaction == null)
            {
                _logger.LogError("Failed to edit status for transaction with order id: {id}", transactionStatusDTO.MerchantOrderId);
                return BadRequest("Inappropriate Transaction Status Or That Transaction Does Not Exist.");
            }
            _logger.LogInformation("Edited status for transaction : {@transaction}", transaction);
            ForwardStatus(transactionStatusDTO);
            return Ok("Successfully edited transaction status.");
        }

        private async void ForwardStatus(TransactionStatusDTO transactionStatusDTO)
        {
            var transactionJson = new StringContent(
              JsonSerializer.Serialize(transactionStatusDTO),
              Encoding.UTF8,
              Application.Json);

            var path = $"{_webHostEnvironment.ContentRootPath}\\clientcertpsp.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage =
            await client.PutAsync("https://localhost:44326/api/transactions", transactionJson);
            httpResponseMessage.Dispose();
        }
    }
}
