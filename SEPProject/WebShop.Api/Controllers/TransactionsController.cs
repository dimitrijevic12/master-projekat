using CSharpFunctionalExtensions;
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
            _logger.LogInformation("Getting all transactions");
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
                _logger.LogError("Failed to create transaction, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created transaction with id: {id}", transaction.Id);
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
            _logger.LogInformation("Edited transaction with id: {id}", 
                transactionDTO.MerchantOrderId);
            return Ok();
        }

        [HttpGet("buyers/{userId}")]
        [Authorize(Roles = "RegisteredUserProxy, AdminProxy")]
        public IActionResult GetTransactionsForBuyer(Guid userId)
        {
            _logger.LogInformation("Getting transactions for buyer: {id}", userId);
            return Ok(_transactionRepository.GetTransactionsForBuyer(userId));
        }

        [HttpGet("sellers/{userId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetTransactionsForSeller(Guid userId)
        {
            _logger.LogInformation("Getting transactions for seller: {id}", userId);
            return Ok(_transactionRepository.GetTransactionsForSeller(userId));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_transactionRepository.GetById(id) is null)
            {
                _logger.LogError("Failed to get transaction by id: {id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting transaction by id: {id}", id);
            return Ok(_transactionRepository.GetById(id));                
        }
    }
}
