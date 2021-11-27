using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.DataAccess.PSPDbContext;

namespace PSP.DataAccess.Implementation
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