using Bank.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PSPRequestsController : Controller
    {
        private readonly IPSPRequestRepository _PSPRequestRepository;

        public PSPRequestsController(IPSPRequestRepository PSPRequestRepository)
        {
            _PSPRequestRepository = PSPRequestRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_PSPRequestRepository.GetAll());
        }
    }
}
