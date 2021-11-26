using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interface.Repository;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationsController : ControllerBase
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationsController(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_accommodationRepository.GetAll());
        }
    }
}
