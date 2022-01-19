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
            _logger.LogInformation("Created Accommodation: {@accommodation}", accommodation);
            return Created(Request.Path + accommodation.Id, "");
        }

        [HttpGet]
        public IActionResult GetAccommodations([FromQuery] string city)
        {
            return Ok(accommodationService.GetAccommodations(city));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Accommodation accommodation = _accommodationRepository.GetById(id);
            return accommodation is null ? BadRequest() : (IActionResult)Ok(accommodation);
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Accommodation accommodation)
        {
            _logger.LogInformation("Edited Accommodation: {@accommodation}", accommodation);
            return Ok(_accommodationRepository.Edit(accommodation));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_accommodationRepository.GetAccommodationsForOwner(ownerId));
        }
    }
}
