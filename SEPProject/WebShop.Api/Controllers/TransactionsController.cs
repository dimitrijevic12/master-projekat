using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionService transactionService;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionRepository transactionRepository,
            TransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpPost]
        [Authorize(Roles = "RegisteredUserProxy, AdminProxy")]
        public IActionResult Save(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            Result result = transactionService.Save(transaction);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create Transaction: {@transaction}, Error: {error}",
                    transaction, result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created Transaction: {@transaction}", transaction);
            return Created(Request.Path + transaction.Id, transaction);
        }

        [HttpPut]
        public IActionResult EditStatus(TransactionDTO transactionDTO)
        {
            if (_transactionRepository.GetById(transactionDTO.MerchantOrderId) is null)
            {
                _logger.LogError("Failed to edit transaction with id: {id}", 
                    transactionDTO.MerchantOrderId);
                return BadRequest();
            }
            Result result = transactionService.EditStatus(transactionDTO);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to edit transaction, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Edited status of Transaction: {@transaction}", 
                transactionDTO);
            return Ok();
        }

        [HttpGet("buyers/{userId}")]
        [Authorize(Roles = "RegisteredUserProxy, AdminProxy")]
        public IActionResult GetTransactionsForBuyer(Guid userId)
        {
            return Ok(_transactionRepository.GetTransactionsForBuyer(userId));
        }

        [HttpGet("sellers/{userId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetTransactionsForSeller(Guid userId)
        {
            return Ok(_transactionRepository.GetTransactionsForSeller(userId));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Transaction transaction = _transactionRepository.GetById(id);
            return transaction is null ? BadRequest() : (IActionResult)Ok(_transactionRepository.GetById(id));
        }

        [HttpPut("per-diem")]
        public IActionResult EditPerdiemStatus(PerdiemTransactionDTO perdiemTransatcionDTO)
        {
            if (_transactionRepository.GetById(perdiemTransatcionDTO.TransactionId) is null)
            {
                _logger.LogError("Failed to edit transaction with id: {id}",
                    perdiemTransatcionDTO.TransactionId);
                return BadRequest();
            }
            Result result = transactionService.EditPerdiemStatus(perdiemTransatcionDTO);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to edit transaction, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Edited perdiem status of Transaction: {@transaction}",
                perdiemTransatcionDTO);
            return Ok();
        }
    }
}
