using Bank.Core.Model;

namespace Bank.Core.Interface.Repository
{
    public interface IRegisteredUserRepository : IRepository<RegisteredUser>
    {
        public RegisteredUser GetByUniquePersonalRegistrationNumber(string UniquePersonalRegistrationNumber);
    }
}