using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSP.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypesController : Controller
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public PaymentTypesController(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        [HttpGet]
        //[Authorize(Roles = "RegisteredWebShopProxy")]
        public IActionResult GetAll()
        {
            return Ok(_paymentTypeRepository.GetAll());
        }
    }
}
