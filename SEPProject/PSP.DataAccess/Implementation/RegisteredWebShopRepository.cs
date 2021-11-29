using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.DataAccess.PSPDbContext;

namespace PSP.DataAccess.Implementation
{
    public class RegisteredWebShopRepository : Repository<RegisteredWebShop>, IRegisteredWebShopRepository
    {
        private AppDbContext dbContext;

        public RegisteredWebShopRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}