using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
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

        public MerchantsController(IMerchantRepository merchantRepository, MerchantService merchantService)
        {
            _merchantRepository = merchantRepository;
            _merchantService = merchantService;
        }

        [HttpPost]
        public IActionResult Save(MerchantDTO merchantDTO)
        {
            merchantDTO.Id = Guid.NewGuid();
            Result result = _merchantService.Save(merchantDTO);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created(Request.Path + merchantDTO.Id, "");
        }     
    }
}
