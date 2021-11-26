using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Interface.Repository;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_itemRepository.GetAll());
        }
    }
}
