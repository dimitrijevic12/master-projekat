using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly CourseService courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository,
            CourseService courseService, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            this.courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Getting future courses from date: {date}", DateTime.Now);
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
                _logger.LogError("Failed to create Course: {@course}, Error: {error}",
                    course, result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created Course: {@course}", course);
            return Created(Request.Path + course.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Course course = _courseRepository.GetById(id);
            if (course is null)
            {
                _logger.LogError("Failed to get course by id: {id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Course: {@course}", course);
            return Ok(course);
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Course course)
        {
            _logger.LogInformation("Edited Course: {@course}", course);
            return Ok(_courseRepository.Edit(course));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            _logger.LogInformation("Getting courses for owner: {id}", ownerId);
            return Ok(_courseRepository.GetCoursesForOwner(ownerId));
        }
    }
}
