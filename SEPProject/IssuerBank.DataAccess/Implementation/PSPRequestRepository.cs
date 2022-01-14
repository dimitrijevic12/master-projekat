using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;

namespace IssuerBank.DataAccess.Implementation
{
    public class PSPRequestRepository : Repository<PSPRequest>, IPSPRequestRepository
    {
        private AppDbContext dbContext;

        public PSPRequestRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}