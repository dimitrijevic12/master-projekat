using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly TransactionService transactinService;

        public TransactionsController(ITransactionRepository transactionRepository,
            TransactionService transactinService)
        {
            _transactionRepository = transactionRepository;
            this.transactinService = transactinService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Save(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            transactinService.Save(transaction);
            return Created(Request.Path + transaction.Id, "");
        }
    }
}
