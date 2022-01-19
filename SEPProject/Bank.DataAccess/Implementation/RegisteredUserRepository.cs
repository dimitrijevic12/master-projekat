using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;
using System.Linq;

namespace Bank.DataAccess.Implementation
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