using System;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface ITransactionItemRepository : IRepository<TransactionItem>
    {
        public Transaction GetTransactionForItem(Guid transactionItemId);
    }
}
