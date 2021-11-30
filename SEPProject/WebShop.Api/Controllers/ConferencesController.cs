using Microsoft.AspNetCore.Mvc;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferencesController(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_conferenceRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Save(Conference conference)
        {
            return Ok(_conferenceRepository.Save(conference));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_conferenceRepository.GetById(id));
        }

        [HttpPut]
        public IActionResult Edit(Conference conference)
        {
            return Ok(_conferenceRepository.Edit(conference));
        }
    }
}
