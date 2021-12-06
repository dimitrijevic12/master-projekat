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
    public class TransportationsController : ControllerBase
    {
        private readonly ITransportationRepository _transportationRepository;
        private readonly TransportationService transportationService;
        private readonly ILogger<TransportationsController> _logger;

        public TransportationsController(ITransportationRepository transportationRepository,
            TransportationService transportationService, ILogger<TransportationsController> logger)
        {
            _transportationRepository = transportationRepository;
            this.transportationService = transportationService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTransportations([FromQuery] string startDestination, 
            [FromQuery] string finalDestination)
        {
            _logger.LogInformation("Getting transportations, start destination: " +
                "{startDestination}, final destination: {finalDestination}", 
                startDestination, finalDestination);
            return Ok(_transportationRepository.GetTransportationsForDestinations(
                startDestination, finalDestination));
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Transportation transportation)
        {
            transportation.Id = Guid.NewGuid();
            Result result = transportationService.Save(transportation);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create transportation, {error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created transportation with id: {id}", transportation.Id);
            return Created(Request.Path + transportation.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_transportationRepository.GetById(id) is null)
            {
                _logger.LogError("Failed to get transportation by id: {id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting transportation by id: {id}", id);
            return Ok(_transportationRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Transportation transportation)
        {
            _logger.LogInformation("Edited transportation by id: {id}", transportation.Id);
            return Ok(_transportationRepository.Edit(transportation));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            _logger.LogInformation("Getting transportations for owner: {id}", ownerId);
            return Ok(_transportationRepository.GetTransportationsForOwner(ownerId));
        }
    }
}
