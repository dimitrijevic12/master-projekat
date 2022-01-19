using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionRepository transactionRepository, TransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Save(TransactionDTO transactionDTO)
        {
            transactionDTO.Id = Guid.NewGuid();
            Result result = _transactionService.Save(transactionDTO);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create transaction : {@transaction}, Error {error}", transactionDTO, result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created transaction : {@transaction}", transactionDTO);
            return Created(Request.Path + transactionDTO.Id, "");
        }

        [HttpGet]
        [Authorize(Roles = "RegisteredWebShopProxy")]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }
    }
}
