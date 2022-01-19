using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication.Certificate;
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
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ItemService itemService;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemRepository itemRepository, 
            ItemService itemService, ILogger<ItemsController> logger)
        {
            _itemRepository = itemRepository;
            this.itemService = itemService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Item item)
        {
            item.ProductKey = Guid.NewGuid();
            Result result = itemService.Save(item);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create Item: {@item}, {error}",
                    item, result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created Item: {@item}", item);
            return Created(Request.Path + item.ProductKey, "");
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Item item)
        {
            _logger.LogInformation("Edited Item: {@item}", item);
            return Ok(_itemRepository.Edit(item));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_itemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Item item = _itemRepository.GetById(id);
            return item is null ? BadRequest() : (IActionResult)Ok(item);
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_itemRepository.GetItemsForOwner(ownerId));
        }
    }
}
