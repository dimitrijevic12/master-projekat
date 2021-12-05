using CardPayment.Core.Interface.Repository;
using CardPayment.Core.Model;
using CardPayment.DataAccess.CardPaymentDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.DataAccess.Implementation
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
