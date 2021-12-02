using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
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

        public MerchantsController(IMerchantRepository merchantRepository, IMerchantService merchantService)
        {
            _merchantRepository = merchantRepository;
            _merchantService = merchantService;
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            Result<Merchant> result = _merchantService.Create(name);
            if (result.IsFailure)
                return BadRequest(result.Error);
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
