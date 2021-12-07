using Bank.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PaymentCardsController> _logger;

        public PaymentCardsController(IPaymentCardRepository paymentCardRepository, ILogger<PaymentCardsController> logger)
        {
            _paymentCardRepository = paymentCardRepository;
            _logger = logger;
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
