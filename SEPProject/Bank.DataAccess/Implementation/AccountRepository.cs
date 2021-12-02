using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Implementation
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private AppDbContext dbContext;

        public AccountRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Account GetByUserId(Guid id) => dbContext.Accounts.ToList().Where(account => account.UserId == id).FirstOrDefault();
    }
}
