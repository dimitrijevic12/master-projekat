using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;

namespace IssuerBank.DataAccess.Implementation
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