using CSharpFunctionalExtensions;
using System;
using System.IO;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Model.File;

namespace WebShop.Core.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Result Save(Item item)
        {
            if (String.IsNullOrEmpty(item.Name) || String.IsNullOrEmpty(item.Price.ToString()))
            {
                return Result.Failure("Name or price can't be empty!");
            }
            _itemRepository.Save(item);
            return Result.Success(item);
        }
    }
}
