

using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;

namespace Bank.DataAccess.Implementation
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
