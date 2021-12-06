using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly AdminService adminService;
        private readonly ILogger<AdminsController> _logger;

        public AdminsController(IAdminRepository adminRepository, AdminService adminService,
            ILogger<AdminsController> logger)
        {
            _adminRepository = adminRepository;
            this.adminService = adminService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Getting all admins");
            return Ok(_adminRepository.GetAll());
        }

        [HttpPut]
        public IActionResult Edit(AdminDTO adminDTO)
        {
            Admin admin = _adminRepository.GetById(adminDTO.AdminId);
            if (admin is null)
            {
                _logger.LogError("Failed to edit admin with id: {id}", adminDTO.AdminId);
                return BadRequest();
            }
            _logger.LogInformation("Edited admin with id: {id}", adminDTO.AdminId);
            admin.MerchantId = adminDTO.MerchantId;
            return Ok(_adminRepository.Edit(admin));
        }

        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            admin.Id = Guid.NewGuid();
            Result result = adminService.Register(admin);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create admin, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created admin with id: {id}", admin.Id);
            return Created(Request.Path + admin.Id, admin);
        }
    }
}
