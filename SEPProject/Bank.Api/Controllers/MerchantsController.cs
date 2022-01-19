using Bank.Api.DTOs;
using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Merchant = Bank.Api.DTOs.Merchant;

namespace Bank.Api.Controllers
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
            Result<Core.Model.Merchant> result = _merchantService.Create(name);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create Merchant, Error: {@Error}", result.Error);
                return BadRequest(result.Error);
            }
            Result<Account> accountResult = _accountService.Create(result.Value.Id);
            _logger.LogInformation("Created Merchant {@Merchant}", new { result.Value.MerchantId, result.Value.Name });
            return Created(this.Request.Path + "/" + result.Value.Id, new Merchant(result.Value.MerchantId, result.Value.MerchantPassword, result.Value.Name));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_merchantRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid merchantId)
        {
            Core.Model.Merchant merchant = _merchantRepository.GetByMerchantId(merchantId);
            Account account = _accountRepository.GetByUserId(merchant.Id);
            return Ok(new MerchantAccount(account.AccountNumber, merchant.Name));
        }
    }
}