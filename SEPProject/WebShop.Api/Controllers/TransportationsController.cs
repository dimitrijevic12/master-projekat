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
    public class TransportationsController : ControllerBase
    {
        private readonly ITransportationRepository _transportationRepository;
        private readonly TransportationService transportationService;

        public TransportationsController(ITransportationRepository transportationRepository,
            TransportationService transportationService)
        {
            _transportationRepository = transportationRepository;
            this.transportationService = transportationService;
        }

        [HttpGet]
        public IActionResult GetTransportations([FromQuery] string startDestination, 
            [FromQuery] string finalDestination)
        {
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
                return BadRequest(result.Error);
            }
            return Created(Request.Path + transportation.Id, "");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return _transportationRepository.GetById(id) is null ?
                BadRequest() :
                Ok(_transportationRepository.GetById(id));
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Transportation transportation)
        {
            return Ok(_transportationRepository.Edit(transportation));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_transportationRepository.GetTransportationsForOwner(ownerId));
        }
    }
}
