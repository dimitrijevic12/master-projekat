using Microsoft.AspNetCore.Mvc;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

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

        [HttpPost]
        public IActionResult Save(Course course)
        {
            return Ok(_courseRepository.Save(course));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_courseRepository.GetById(id));
        }

        [HttpPut]
        public IActionResult Edit(Course course)
        {
            return Ok(_courseRepository.Edit(course));
        }
    }
}
