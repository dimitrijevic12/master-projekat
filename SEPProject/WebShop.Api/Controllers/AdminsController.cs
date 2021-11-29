using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            admin.Id = Guid.NewGuid();
            if (adminService.Register(admin) == null)
            {
                return BadRequest();
            }
            return Created(Request.Path + admin.Id, "");
        }
    }
}
