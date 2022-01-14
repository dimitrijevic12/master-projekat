using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;
using System;
using System.Linq;

namespace IssuerBank.DataAccess.Implementation
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