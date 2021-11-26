using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class TransactionItemRepository : Repository<TransactionItem>, ITransactionItemRepository
    {
        private AppDbContext dbContext;

        public TransactionItemRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Transaction GetTransactionForItem(Guid transactionItemId)
        {
            return GetById(transactionItemId).Transaction;
        }
    }
}
