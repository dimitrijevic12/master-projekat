using CardPayment.Core.DTOs;
using CardPayment.Core.Interface.Repository;
using CardPayment.Core.Services;
using Microsoft.AspNetCore.Mvc;
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

        public TransactionsController(ITransactionRepository transactionRepository, TransactionService transactionService, IHttpClientFactory httpClientFactory)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{OrderId}")]
        public IActionResult GenerateRequestForBank(Guid orderId)
        {
            var request = _transactionService.CreateRequestForBank(orderId);
            if (request == null) return BadRequest();
            return Ok(request);
        }

        [HttpPut]
        public IActionResult SetTransactionsPaymentId(TransactionsPaymentIdDTO transactionsPaymentIdDTO)
        {
            if(_transactionService.SetTransactionsPaymentId(transactionsPaymentIdDTO) == null) return BadRequest();
            return Ok("Successfully edited payment id.");
        }

        [HttpPut]
        public IActionResult EditTransactionStatus(TransactionStatusDTO transactionStatusDTO)
        {
            if (_transactionService.EditTransaction(transactionStatusDTO) == null)
            {
                return BadRequest();
            }
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
