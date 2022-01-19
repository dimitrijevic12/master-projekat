using CSharpFunctionalExtensions;
using IssuerBank.Api.DTOs;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace IssuerBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : Controller
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMerchantService _merchantService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;
        private readonly ILogger<MerchantsController> _logger;

        public MerchantsController(IMerchantRepository merchantRepository, IMerchantService merchantService, IAccountRepository accountRepository,
            IAccountService accountService, ILogger<MerchantsController> logger)
        {
            _merchantRepository = merchantRepository;
            _merchantService = merchantService;
            _accountRepository = accountRepository;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create([FromBody] string name)
        {
            Result<Merchant> result = _merchantService.Create(name);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create Merchant, Error: {@Error}", result.Error);
                return BadRequest(result.Error);
            }
            Result<Account> accountResult = _accountService.Create(result.Value.Id);
            _logger.LogInformation("Created Merchant {@Merchant}", new { result.Value.MerchantId, result.Value.Name });
            return Created(this.Request.Path + "/" + result.Value.Id, result.Value);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_merchantRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid merchantId)
        {
            Merchant merchant = _merchantRepository.GetByMerchantId(merchantId);
            Account account = _accountRepository.GetByUserId(merchant.Id);
            return Ok(new MerchantAccount(account.AccountNumber, merchant.Name));
        }
    }
}