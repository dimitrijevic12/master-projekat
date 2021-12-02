using Microsoft.AspNetCore.Mvc;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using System;

namespace PSP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : Controller
    {
        private readonly IMerchantRepository _merchantRepository;
       

        public MerchantsController(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        [HttpPost]
        public IActionResult Save(MerchantDTO merchantDTO)
        {
            merchantDTO.Id = Guid.NewGuid();
            _merchantRepository.Save(new Merchant(merchantDTO.Id, merchantDTO.MerchantId, merchantDTO.MerchantPassword, merchantDTO.Name, merchantDTO.RegisteredWebShopId));
            return Created(Request.Path + merchantDTO.Id, "");
        }     
    }
}
