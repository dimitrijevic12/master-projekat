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
    }
}
