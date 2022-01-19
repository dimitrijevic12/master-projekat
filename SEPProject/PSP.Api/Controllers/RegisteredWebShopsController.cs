using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RegisteredWebShopsController> _logger;

        public RegisteredWebShopsController(IRegisteredWebShopRepository registeredWebShopRepository, RegisteredWebShopService registeredWebShopService,
            PaymentTypeRegisteredWebShopService paymentTypeRegisteredWebShopService, ILogger<RegisteredWebShopsController> logger)
        {
            _registeredWebShopRepository = registeredWebShopRepository;
            _registeredWebShopService = registeredWebShopService;
            _paymentTypeRegisteredWebShopService = paymentTypeRegisteredWebShopService;
            _logger = logger;
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
                _logger.LogInformation("Login for webshop with email: {email}", login.Email);
                return response;
            }
            _logger.LogError("Failed to login  webshop with email: {email}", login.Email);
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
            RegisteredWebShop webShop = _registeredWebShopService.GetWebShopByEmail(email);
            if (webShop == null)
            {
                return BadRequest();
            }
            return Ok(webShop);
        }

        [HttpPost]
        public IActionResult Save(RegisteredWebShopDTO registeredWebShopDTO)
        {
            registeredWebShopDTO.Id = Guid.NewGuid();
            Result result = _registeredWebShopService.Save(registeredWebShopDTO);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create webshop : {@webshop}, Error : {error}", registeredWebShopDTO, result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created webshop : {@webshop}", registeredWebShopDTO);
            return Created(Request.Path + registeredWebShopDTO.Id, "");
        }

        [HttpPut]
        public IActionResult EditPaymentTypes(PaymentTypeRegisteredWebShopDTO paymentTypeRegisteredWebShopDTO)
        {
            RegisteredWebShop webShop = _paymentTypeRegisteredWebShopService.Save(paymentTypeRegisteredWebShopDTO);
            if (webShop == null)
            {
                _logger.LogError("Failed to edit webshop with id: {id}", paymentTypeRegisteredWebShopDTO.RegisteredWebShopId);
                return BadRequest();
            }
            _logger.LogInformation("Edited payment types for webshop : {@webshop}", webShop);
            return Ok("Successfully edited payment types for this Web shop.");
        }
    }
}