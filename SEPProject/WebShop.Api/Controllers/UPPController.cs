using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPPController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly TransactionService transactionService;

        public UPPController(ITransactionRepository transactionRepository,
            TransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            this.transactionService = transactionService;
        }

        [HttpPost]
        public IActionResult Save(UPPItemTransaction uppItemTransaction)
        {
            transactionService.SaveUPPItemTransaction(uppItemTransaction);
            return Ok(uppItemTransaction);
        }
    }
}
