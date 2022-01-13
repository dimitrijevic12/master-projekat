using IssuerBank.Core.Interface.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IssuerBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredUsersController : Controller
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