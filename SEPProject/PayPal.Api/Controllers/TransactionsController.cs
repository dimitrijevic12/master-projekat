using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayPal.Core.DTOs;
using PayPal.Core.Interface.Repository;
using PayPal.Core.Model;
using PayPal.Core.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace PayPal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionService _transactionService;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionRepository transactionRepository, TransactionService transactionService, 
            IHttpClientFactory httpClientFactory, ILogger<TransactionsController> logger)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public IActionResult GetTransactionByOrderId(Guid orderId)
        {
            Transaction transaction = _transactionRepository.GetTransactionByOrderId(orderId);
            if (transaction == null)
            {
                return BadRequest("Inappropriate Transaction Status Or That Transaction Does Not Exist.");
            }
            return Ok(transaction);
        }

        [HttpPut]
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

            HttpClient client = _httpClientFactory.CreateClient();
            using var httpResponseMessage =
            await client.PutAsync("https://localhost:44326/api/transactions", transactionJson);
            httpResponseMessage.Dispose();
        }
    }
}
