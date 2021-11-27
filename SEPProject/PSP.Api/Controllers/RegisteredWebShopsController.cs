using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PSP.Core.Interface.Repository;
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

        public RegisteredWebShopsController(IRegisteredWebShopRepository registeredWebShopRepository)
        {
            _registeredWebShopRepository = registeredWebShopRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_registeredWebShopRepository.GetAll());
        }
    }
}