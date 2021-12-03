using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TransactionsController(ITransactionRepository transactionRepository,
            TransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            this.transactionService = transactionService;
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
                return BadRequest(result.Error);
            }
            return Created(Request.Path + transaction.Id, transaction);
        }

        [HttpPut]
        public IActionResult EditStatus(TransactionDTO transactionDTO)
        {
            if (_transactionRepository.GetById(transactionDTO.TransactionId) is null)
            {
                return BadRequest();
            }
            Result result = transactionService.EditStatus(transactionDTO);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
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
            return _transactionRepository.GetById(id) is null ?
                BadRequest() :
                Ok(_transactionRepository.GetById(id));
        }
    }
}
