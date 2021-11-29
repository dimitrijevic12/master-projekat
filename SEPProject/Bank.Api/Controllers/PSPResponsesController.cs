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
    public class PSPResponsesController : Controller
    {
        private readonly IPSPResponseRepository _PSPResponseRepository;

        public PSPResponsesController(IPSPResponseRepository pSPResponseRepository)
        {
            _PSPResponseRepository = pSPResponseRepository;
        }
    }
}
