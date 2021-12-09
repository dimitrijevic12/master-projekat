using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Result<Account> Create(Guid MerchantId)
        {
            Guid id = Guid.NewGuid();
            while (_accountRepository.GetById(id) != null)
                id = Guid.NewGuid();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            string accountNumber = "";
            for(int i = 0; i < 10; i++)
            {
                accountNumber += rnd.Next(10).ToString();
            }
            while(_accountRepository.GetByAccountNumber(accountNumber) != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    accountNumber += rnd.Next(10).ToString();
                }
            }
            return _accountRepository.Save(new Account(id, accountNumber, 0, MerchantId));
        }
    }
}
