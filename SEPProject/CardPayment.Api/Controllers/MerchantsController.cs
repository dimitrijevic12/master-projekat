using CardPayment.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPayment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : Controller
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogger<MerchantsController> _logger;

        public MerchantsController(IMerchantRepository merchantRepository, ILogger<MerchantsController> logger)
        {
            _merchantRepository = merchantRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_merchantRepository.GetAll());
        }
    }
}
