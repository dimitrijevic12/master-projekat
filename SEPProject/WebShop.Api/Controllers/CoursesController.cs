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
                _logger.LogError("Failed to create course, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created course with id: {id}", course.Id);
            return Created(Request.Path + course.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_courseRepository.GetById(id) is null)
            {
                _logger.LogError("Failed to get course by id: {id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting course by id: {id}", id);
            return Ok(_courseRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Course course)
        {
            _logger.LogInformation("Edited course by id: {id}", course.Id);
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
