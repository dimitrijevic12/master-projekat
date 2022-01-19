using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;
using System.Linq;

namespace IssuerBank.DataAccess.Implementation
{
    public class RegisteredUserRepository : Repository<RegisteredUser>, IRegisteredUserRepository
    {
        private AppDbContext dbContext;

        public RegisteredUserRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public RegisteredUser GetByUniquePersonalRegistrationNumber(string uniquePersonalRegistrationNumber)
        {
            return dbContext.RegisteredUsers.Where(user => user.UniquePersonalRegistrationNumber.Equals(uniquePersonalRegistrationNumber))
                .FirstOrDefault();
        }
    }
}