using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
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

        public AdminsController(IAdminRepository adminRepository, AdminService adminService)
        {
            _adminRepository = adminRepository;
            this.adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_adminRepository.GetAll());
        }

        [HttpPut]
        public IActionResult Edit(AdminDTO adminDTO)
        {
            Admin admin = _adminRepository.GetById(adminDTO.AdminId);
            if (admin is null)
            {
                return BadRequest();
            }
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
                return BadRequest(result.Error);
            }
            return Created(Request.Path + admin.Id, admin);
        }
    }
}
