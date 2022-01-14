using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Interface.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account GetByUserId(Guid id);
        public Account GetByAccountNumber(string accountNumber);
    }
}
