using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
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
    public class MerchantsController : Controller
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMerchantService _merchantService;
        private readonly IAccountService _accountService;
        private readonly ILogger<MerchantsController> _logger;

        public MerchantsController(IMerchantRepository merchantRepository, IMerchantService merchantService, IAccountService accountService,
            ILogger<MerchantsController> logger)
        {
            _merchantRepository = merchantRepository;
            _merchantService = merchantService;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] string name)
        {
            Result<Merchant> result = _merchantService.Create(name);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create Merchant {@Merchant}, Error: {@Error}", name, result.Error);
                return BadRequest(result.Error);
            }
            Result<Account> accountResult = _accountService.Create(result.Value.Id);
            _logger.LogInformation("Created Merchant {@Merchant}", result.Value);
            return Created(this.Request.Path + "/" + result.Value.Id, result.Value);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_merchantRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_merchantRepository.GetById(id));
        }
    }
}
