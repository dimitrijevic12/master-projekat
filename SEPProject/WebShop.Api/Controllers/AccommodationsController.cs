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
    public class AccommodationsController : ControllerBase
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly AccommodationService accommodationService;

        public AccommodationsController(IAccommodationRepository accommodationRepository,
            AccommodationService accommodationService)
        {
            _accommodationRepository = accommodationRepository;
            this.accommodationService = accommodationService;
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Accommodation accommodation)
        {
            return Ok(_accommodationRepository.Save(accommodation));
        }

        [HttpGet]
        public IActionResult GetAccommodations([FromQuery] string city)
        {
            return Ok(accommodationService.GetAccommodations(city));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return _accommodationRepository.GetById(id) is null ?
                BadRequest() :
                Ok(_accommodationRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Accommodation accommodation)
        {
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
