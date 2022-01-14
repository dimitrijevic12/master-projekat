using CryptoValute.Core.Interface.Repository;
using CryptoValute.Core.Model;
using CryptoValute.DataAccess.CryptoValuteDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.DataAccess.Implementation
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
