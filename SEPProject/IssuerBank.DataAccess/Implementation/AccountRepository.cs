using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;
using System;
using System.Linq;

namespace IssuerBank.DataAccess.Implementation
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private AppDbContext dbContext;

        public AccountRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Account GetByAccountNumber(string accountNumber) => dbContext.Accounts.ToList()
            .Where(account => account.AccountNumber.Equals(accountNumber)).FirstOrDefault();

        public Account GetByUserId(Guid id) => dbContext.Accounts.ToList().Where(account => account.UserId == id).FirstOrDefault();
    }
}