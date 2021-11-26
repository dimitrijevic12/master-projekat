using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interface.Repository;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredUsersController : ControllerBase
    {
        private readonly IRegisteredUserRepository _registeredUserRepository;

        public RegisteredUsersController(IRegisteredUserRepository registeredUserRepository)
        {
            _registeredUserRepository = registeredUserRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_registeredUserRepository.GetAll());
        }
    }
}
