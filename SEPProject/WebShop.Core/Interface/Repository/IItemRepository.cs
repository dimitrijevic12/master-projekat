using System;
using System.Collections.Generic;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface IItemRepository : IRepository<Item>
    {
        public IEnumerable<Item> GetItemsForOwner(Guid ownerId);
        public Item GetItemByName(string name);
    }
}
