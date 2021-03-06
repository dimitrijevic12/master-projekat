using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interface.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account GetByUserId(Guid id);
        public Account GetByAccountNumber(string accountNumber);
    }
}
