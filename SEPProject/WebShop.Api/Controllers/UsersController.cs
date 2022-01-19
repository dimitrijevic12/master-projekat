using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebShop.Core.DTOs;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            this.userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel login)
        {
            IActionResult response = Unauthorized();
            User user = userService.FindUser(login.Username, login.Password);
            if (user != null)
            {
                string tokenString = userService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
                return response;
            }
            return response;
        }
    }
}
