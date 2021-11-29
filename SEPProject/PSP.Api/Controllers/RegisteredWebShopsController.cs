using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredWebShopsController : Controller
    {
        private readonly IRegisteredWebShopRepository _registeredWebShopRepository;
        private readonly PaymentTypeRegisteredWebShopService _paymentTypeRegisteredWebShopService;     
        private readonly RegisteredWebShopService _registeredWebShopService;
        
        public RegisteredWebShopsController(IRegisteredWebShopRepository registeredWebShopRepository, RegisteredWebShopService registeredWebShopService,
            PaymentTypeRegisteredWebShopService paymentTypeRegisteredWebShopService)
        {
            _registeredWebShopRepository = registeredWebShopRepository;
            _registeredWebShopService = registeredWebShopService;
            _paymentTypeRegisteredWebShopService = paymentTypeRegisteredWebShopService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_registeredWebShopRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Save(RegisteredWebShopDTO registeredWebShopDTO)
        {
            registeredWebShopDTO.Id = Guid.NewGuid();
            _registeredWebShopService.Save(registeredWebShopDTO);
            return Created(Request.Path + registeredWebShopDTO.Id, "");
        }

        [HttpPut]
        public IActionResult EditPaymentTypes(PaymentTypeRegisteredWebShopDTO paymentTypeRegisteredWebShopDTO)
        {
            _paymentTypeRegisteredWebShopService.Save(paymentTypeRegisteredWebShopDTO);
            return Ok("Successfully edited payment types for this Web shop.");
        }
    }
}