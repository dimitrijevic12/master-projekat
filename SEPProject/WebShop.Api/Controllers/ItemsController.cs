using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ItemService itemService;
        private readonly IWebHostEnvironment _env;

        public ItemsController(IItemRepository itemRepository, ItemService itemService, 
            IWebHostEnvironment env)
        {
            _itemRepository = itemRepository;
            _env = env;
            this.itemService = itemService;
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
    }
}
