using PayPal.Core.Model;
using System;

namespace PayPal.Core.Interface.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public Transaction GetTransactionByOrderId(Guid orderId);
    }
}
