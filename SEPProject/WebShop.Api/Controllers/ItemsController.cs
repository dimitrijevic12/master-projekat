using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IWebHostEnvironment _env;

        public ItemsController(IItemRepository itemRepository, IWebHostEnvironment env)
        {
            _itemRepository = itemRepository;
            _env = env;
        }

        [HttpPost]
        public IActionResult Save(Item item)
        {
            item.ProductKey = Guid.NewGuid();
            _itemRepository.Save(item);
            return Created(Request.Path + item.ProductKey, "");
        }

        [HttpPut]
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
            return Ok(_itemRepository.GetById(id));
        }

        [HttpGet("users/{ownerId}")]
        public IActionResult GetForOwner(Guid ownerId)
        {
            return Ok(_itemRepository.GetItemsForOwner(ownerId));
        }
    }
}
