using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly CourseService courseService;

        public CoursesController(ICourseRepository courseRepository,
            CourseService courseService)
        {
            _courseRepository = courseRepository;
            this.courseService = courseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_courseRepository.GetFutureCourses());
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Course course)
        {
            course.Id = Guid.NewGuid();
            Result result = courseService.Save(course);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created(Request.Path + course.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return _courseRepository.GetById(id) is null ?
                BadRequest() :
                Ok(_courseRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Course course)
        {
            return Ok(_courseRepository.Edit(course));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_courseRepository.GetCoursesForOwner(ownerId));
        }
    }
}
