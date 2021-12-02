using System;
using System.Collections.Generic;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public IEnumerable<Transaction> GetTransactionsForBuyer(Guid userId);
    }
}
