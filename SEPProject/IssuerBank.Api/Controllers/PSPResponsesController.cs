using IssuerBank.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IssuerBank.Api.Controllers
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