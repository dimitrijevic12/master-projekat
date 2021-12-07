using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.Core.Services;
using System;

namespace PSP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : Controller
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly MerchantService _merchantService;
        private readonly ILogger<MerchantsController> _logger;

        public MerchantsController(IMerchantRepository merchantRepository, MerchantService merchantService, ILogger<MerchantsController> logger)
        {
            _merchantRepository = merchantRepository;
            _merchantService = merchantService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Save(MerchantDTO merchantDTO)
        {
            merchantDTO.Id = Guid.NewGuid();
            Result result = _merchantService.Save(merchantDTO);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create merchant, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created merchant with id: {id}", merchantDTO.Id);
            return Created(Request.Path + merchantDTO.Id, "");
        }     
    }
}
