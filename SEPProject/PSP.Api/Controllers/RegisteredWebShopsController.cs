using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
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

        [HttpPost("login")]
        public IActionResult Login(UserModel login)
        {
            IActionResult response = Unauthorized();
            RegisteredWebShop webShop = _registeredWebShopService.FindWebShop(login.Email, login.Password);
            if (webShop != null)
            {
                string tokenString = _registeredWebShopService.GenerateJSONWebToken(webShop);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        
        [HttpGet]
        [Authorize(Roles = "RegisteredWebShopProxy")]
        public IActionResult GetAll()
        {
            return Ok(_registeredWebShopRepository.GetAll());
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "RegisteredWebShopProxy")]
        public IActionResult GetWebShopByEmail(string email)
        {
            var webShop = _registeredWebShopService.GetWebShopByEmail(email);
            if (webShop == null) return BadRequest();
            return Ok(webShop);
        }

        [HttpPost]
        public IActionResult Save(RegisteredWebShopDTO registeredWebShopDTO)
        {
            registeredWebShopDTO.Id = Guid.NewGuid();
            if (_registeredWebShopService.Save(registeredWebShopDTO) == null)
            {
                return BadRequest();
            }         
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