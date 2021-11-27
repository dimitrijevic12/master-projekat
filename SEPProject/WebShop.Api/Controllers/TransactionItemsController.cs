using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionItemsController : ControllerBase
    {
        private readonly ITransactionItemRepository _transactionItemRepository;

        public TransactionItemsController(ITransactionItemRepository transactionItemRepository)
        {
            _transactionItemRepository = transactionItemRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transactionItemRepository.GetAll());
        }

        [HttpGet("transactionForItem")]
        public IActionResult GetTransactionForTransactionItem()
        {
            return Ok(_transactionItemRepository.GetTransactionForItem(new Guid("12345678-1234-1234-1234-123412341234")));
        }

        [HttpPost]
        public IActionResult Save(TransactionItem transactionItem)
        {
            transactionItem.Id = Guid.NewGuid();
            _transactionItemRepository.Save(transactionItem);
            return Created(Request.Path + transactionItem.Id, "");
        }
    }
}
