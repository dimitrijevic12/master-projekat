using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private AppDbContext dbContext;

        public AdminRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
