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
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ConferenceService conferenceService;
        private readonly ILogger<ConferencesController> _logger;

        public ConferencesController(IConferenceRepository conferenceRepository,
            ConferenceService conferenceService, ILogger<ConferencesController> logger)
        {
            _conferenceRepository = conferenceRepository;
            this.conferenceService = conferenceService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Getting future conferences from date: {date}", DateTime.Now);
            return Ok(_conferenceRepository.GetFutureConferences());
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Conference conference)
        {
            conference.Id = Guid.NewGuid();
            Result result = conferenceService.Save(conference);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create conference, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created conference with id: {id}", conference.Id);
            return Created(Request.Path + conference.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_conferenceRepository.GetById(id) is null)
            {
                _logger.LogError("Failed to get conference by id: {id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting conference by id: {id}", id);
            return Ok(_conferenceRepository.GetById(id));
                
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Conference conference)
        {
            _logger.LogInformation("Edited conference by id: {id}", conference.Id);
            return Ok(_conferenceRepository.Edit(conference));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            _logger.LogInformation("Getting conferences for owner: {id}", ownerId);
            return Ok(_conferenceRepository.GetConferencesForOwner(ownerId));
        }
    }
}
