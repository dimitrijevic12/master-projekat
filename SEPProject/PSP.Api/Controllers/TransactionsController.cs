using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PSP.Api.Controllers
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

        [HttpPost]
        public IActionResult Save(TransactionDTO transactionDTO)
        {
            transactionDTO.Id = Guid.NewGuid();
            _transactionRepository.Save(new Transaction(transactionDTO.Id, transactionDTO.Amount, transactionDTO.Timestamp, transactionDTO.OrderId,
                transactionDTO.TransactionStatus, transactionDTO.MerchantId, transactionDTO.MerchantName, transactionDTO.IssuerId, transactionDTO.IssuerName));
            ForwardTransaction(_transactionService.CreateTransactionForBank(transactionDTO));
            return Created(Request.Path + transactionDTO.Id, "");
        }

        [HttpGet]
        [Authorize(Roles = "RegisteredWebShopProxy")]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpPut]
        public IActionResult EditTransactionStatus(TransactionStatusDTO transactionStatusDTO)
        {
            if(_transactionService.EditTransaction(transactionStatusDTO) == null)
            {
                return BadRequest();
            }
            ForwardStatus(transactionStatusDTO);
            return Ok("Successfully edited transaction status.");
        }

        private async void ForwardTransaction(RequestDTO requestDTO)
        {
            var requestJson = new StringContent(
              JsonSerializer.Serialize(requestDTO),
              Encoding.UTF8,
              Application.Json);

            HttpClient client = _httpClientFactory.CreateClient();
            using var httpResponseMessage =
            await client.PostAsync("https://localhost:5001/api/PSPRequests", requestJson);
            httpResponseMessage.Dispose();
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
