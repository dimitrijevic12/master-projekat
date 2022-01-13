using IssuerBank.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IssuerBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid merchantId)
        {
            return Ok(_accountRepository.GetAll());
        }
    }
}