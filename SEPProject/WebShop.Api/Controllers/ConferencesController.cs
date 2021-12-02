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
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ConferenceService conferenceService;

        public ConferencesController(IConferenceRepository conferenceRepository,
            ConferenceService conferenceService)
        {
            _conferenceRepository = conferenceRepository;
            this.conferenceService = conferenceService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_conferenceRepository.GetAll());
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Conference conference)
        {
            conference.Id = Guid.NewGuid();
            Result result = conferenceService.Save(conference);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created(Request.Path + conference.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return _conferenceRepository.GetById(id) is null ? 
                BadRequest() : 
                Ok(_conferenceRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Conference conference)
        {
            return Ok(_conferenceRepository.Edit(conference));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_conferenceRepository.GetConferencesForOwner(ownerId));
        }
    }
}
