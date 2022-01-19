using CSharpFunctionalExtensions;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RegisteredUsersController> _logger;

        public RegisteredUsersController(IRegisteredUserRepository registeredUserRepository,
            RegisteredUserService registeredUserService, 
            ILogger<RegisteredUsersController> logger)
        {
            _registeredUserRepository = registeredUserRepository;
            this.registeredUserService = registeredUserService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
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
                _logger.LogError("Failed to create Registered user: {@registeredUser}, {error}",
                    registeredUser, result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created Registered user: {@registeredUser}", registeredUser);
            return Created(Request.Path + registeredUser.Id, "");
        }
    }
}
