using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CryptoValute.Core.DTOs;
using CryptoValute.Core.Interface.Repository;
using CryptoValute.Core.Model;
using CryptoValute.Core.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace CryptoValute.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionService _transactionService;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransactionsController(ITransactionRepository transactionRepository, TransactionService transactionService,
            IHttpClientFactory httpClientFactory, ILogger<TransactionsController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("cryptoPayment")]
        public async Task<CryptoPaymentDTO> PayWithCryptoValute(CryptoRequestDTO cryptoRequestDTO)
        {
            var requestJson = new StringContent(
              JsonSerializer.Serialize(cryptoRequestDTO),
              Encoding.UTF8,
              Application.Json);

            HttpClient client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "C59hiSij8TSsJvJwvy36f7fFpWy7sqyeqzBfWjzE");
            using var httpResponseMessage =
            await client.PostAsync("https://api-sandbox.coingate.com/v2/orders", requestJson);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            response = response.Substring(response.IndexOf("https:"));
            response = response.Substring(0, response.IndexOf("underpaid_amount")-3);
            httpResponseMessage.Dispose();
            CryptoPaymentDTO cryptoPaymentDTO = new CryptoPaymentDTO(response);
            return cryptoPaymentDTO;
        }

        [HttpGet("{orderId}")]
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

            var path = $"{_webHostEnvironment.ContentRootPath}\\psp.pfx";
            HttpClient client = new HttpClient(HTTPClientHandlerFactory.Create(path));
            using var httpResponseMessage =
            await client.PutAsync("https://172.20.10.2:44326/api/transactions", transactionJson);
            httpResponseMessage.Dispose();
        }

       
    }
}
