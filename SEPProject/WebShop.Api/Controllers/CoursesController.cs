using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interface.Repository;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_courseRepository.GetAll());
        }
    }
}
