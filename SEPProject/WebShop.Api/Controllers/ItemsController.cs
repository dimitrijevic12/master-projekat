using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        public ItemsController(IItemRepository itemRepository, 
            ItemService itemService)
        {
            _itemRepository = itemRepository;
            this.itemService = itemService;
        }

        [HttpPost]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Save(Item item)
        {
            item.ProductKey = Guid.NewGuid();
            Result result = itemService.Save(item);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Created(Request.Path + item.ProductKey, "");
        }

        [HttpPut]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult Edit(Item item)
        {
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
            return _itemRepository.GetById(id) is null ?
                BadRequest() :
                Ok(_itemRepository.GetById(id));
        }

        [HttpGet("users/{ownerId}")]
        [Authorize(Roles = "AdminProxy")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_itemRepository.GetItemsForOwner(ownerId));
        }
    }
}
