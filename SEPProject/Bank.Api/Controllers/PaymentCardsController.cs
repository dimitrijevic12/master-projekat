using Bank.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCardsController : Controller
    {
        private readonly IPaymentCardRepository _paymentCardRepository;

        public PaymentCardsController(IPaymentCardRepository paymentCardRepository)
        {
            _paymentCardRepository = paymentCardRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_paymentCardRepository.GetAll());
        }

        [HttpGet("{id}/owner")]
        public IActionResult GetOwner(string id)
        {
            return Ok(_paymentCardRepository.GetById(new Guid(id)).CardOwner);
        }
    }
}
