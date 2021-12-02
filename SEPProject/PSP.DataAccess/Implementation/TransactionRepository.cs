using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.DataAccess.PSPDbContext;
using System;
using System.Linq;

namespace PSP.DataAccess.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private AppDbContext dbContext;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Transaction GetTransactionByOrderId(Guid orderId)
        {
            return dbContext.Transactions.ToList().FirstOrDefault(transaction => transaction.OrderId == orderId);
        }
    }
}