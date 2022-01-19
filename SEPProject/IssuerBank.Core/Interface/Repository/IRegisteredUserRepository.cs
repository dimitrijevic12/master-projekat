using IssuerBank.Core.Model;

namespace IssuerBank.Core.Interface.Repository
{
    public interface IRegisteredUserRepository : IRepository<RegisteredUser>
    {
        public RegisteredUser GetByUniquePersonalRegistrationNumber(string uniquePersonalRegistrationNumber);
    }
}