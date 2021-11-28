using CSharpFunctionalExtensions;
using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class RegisteredUserRepository : Repository<RegisteredUser>, IRegisteredUserRepository
    {
        private AppDbContext dbContext;

        public RegisteredUserRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
