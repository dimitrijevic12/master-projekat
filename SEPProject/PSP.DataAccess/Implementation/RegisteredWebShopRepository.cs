using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.DataAccess.PSPDbContext;
using System.Linq;

namespace PSP.DataAccess.Implementation
{
    public class RegisteredWebShopRepository : Repository<RegisteredWebShop>, IRegisteredWebShopRepository
    {
        private AppDbContext dbContext;

        public RegisteredWebShopRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public RegisteredWebShop GetByEmail(string email)
        {
            return dbContext.RegisteredWebShops.ToList().FirstOrDefault(registeredWebShop => registeredWebShop.EmailAddress.Equals(email));                     
        }
    }
}