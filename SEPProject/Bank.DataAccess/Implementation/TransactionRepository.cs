

using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;
using System;
using System.Linq;

namespace Bank.DataAccess.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private AppDbContext dbContext;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Transaction GetByPaymentId(Guid paymentId) => dbContext.Transactions.ToList()
            .Where(transaction => transaction.PaymentId == paymentId).FirstOrDefault();
    }
}
