using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredUsersController : ControllerBase
    {
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly RegisteredUserService registeredUserService;

        public RegisteredUsersController(IRegisteredUserRepository registeredUserRepository,
            RegisteredUserService registeredUserService)
        {
            _registeredUserRepository = registeredUserRepository;
            this.registeredUserService = registeredUserService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_registeredUserRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Register(RegisteredUser registeredUser)
        {
            registeredUser.Id = Guid.NewGuid();
            Result result = registeredUserService.Register(registeredUser);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created(Request.Path + registeredUser.Id, "");
        }
    }
}
