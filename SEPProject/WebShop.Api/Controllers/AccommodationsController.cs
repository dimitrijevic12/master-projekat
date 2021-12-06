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
    public class AccommodationsController : ControllerBase
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly AccommodationService accommodationService;
        private readonly ILogger<AccommodationsController> _logger;

        public AccommodationsController(IAccommodationRepository accommodationRepository,
            AccommodationService accommodationService, ILogger<AccommodationsController> logger)
        {
            _accommodationRepository = accommodationRepository;
            this.accommodationService = accommodationService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Accommodation accommodation)
        {
            Result result = accommodationService.Save(accommodation);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create accommodation, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created accommodation with id: {id}", accommodation.Id);
            return Created(Request.Path + accommodation.Id, "");
        }

        [HttpGet]
        public IActionResult GetAccommodations([FromQuery] string city)
        {
            _logger.LogInformation("Getting accommodation by city: {city}", city);
            return Ok(accommodationService.GetAccommodations(city));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_accommodationRepository.GetById(id) is null)
            {
                _logger.LogError("Failed to get accommodation by id: {id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting accommodation by id: {id}", id);
            return Ok(_accommodationRepository.GetById(id));
                
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Accommodation accommodation)
        {
            _logger.LogInformation("Edited accommodation by id: {id}", accommodation.Id);
            return Ok(_accommodationRepository.Edit(accommodation));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            _logger.LogInformation("Getting accommodations for owner: {id}", ownerId);
            return Ok(_accommodationRepository.GetAccommodationsForOwner(ownerId));
        }
    }
}
