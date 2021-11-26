using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interface.Repository;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationsController : ControllerBase
    {
        private readonly ITransportationRepository _transportationRepository;

        public TransportationsController(ITransportationRepository transportationRepository)
        {
            _transportationRepository = transportationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transportationRepository.GetAll());
        }
    }
}
