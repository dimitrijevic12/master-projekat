using PayPal.Core.Interface.Repository;
using PayPal.Core.Model;
using PayPal.DataAccess.PayPalDbContext;
using System;
using System.Linq;

namespace PayPal.DataAccess.Implementation
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
