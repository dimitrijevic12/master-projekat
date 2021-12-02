﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private AppDbContext dbContext;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Transaction> GetTransactionsForBuyer(Guid userId)
        {
            return dbContext.Transactions.ToList().Where(transaction => transaction.BuyerId == userId).ToList();
        }
    }
}
