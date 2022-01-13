using PCC.Core.Interface.Repository;
using PCC.Core.Model;
using PCC.DataAccess.PCCDbContext;

namespace PCC.DataAccess.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private AppDbContext dbContext;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}