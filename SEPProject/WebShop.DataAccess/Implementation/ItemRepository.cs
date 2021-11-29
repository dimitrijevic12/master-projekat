using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private AppDbContext dbContext;

        public ItemRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Item> GetItemsForOwner(Guid ownerId)
        {
            return dbContext.Items.ToList().Where(item => item.OwnerId == ownerId).ToList();
        }
    }
}
