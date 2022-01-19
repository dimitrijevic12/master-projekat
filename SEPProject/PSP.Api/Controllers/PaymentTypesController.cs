using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.Core.Services;
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
        private readonly PaymentTypeService _paymentTypeService;
        private readonly ILogger<PaymentTypesController> _logger;

        public PaymentTypesController(IPaymentTypeRepository paymentTypeRepository, PaymentTypeService paymentTypeService, ILogger<PaymentTypesController> logger)
        {
            _paymentTypeRepository = paymentTypeRepository;
            _paymentTypeService = paymentTypeService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CertificateAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult GetAll()
        {
            return Ok(_paymentTypeRepository.GetAll());
        }

        [HttpGet("{orderId}")]
        public IActionResult GetPaymentTypesForWebShopByOrderId(Guid orderId)
        {
            ICollection<PaymentType>  paymentTypes = _paymentTypeService.GetPaymentTypesForWebShopByOrderId(orderId);
            if (paymentTypes == null)
            {
                return BadRequest();
            }
            return Ok(paymentTypes);
        }
    }
}
