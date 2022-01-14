using CSharpFunctionalExtensions;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using System;

namespace IssuerBank.Core.Services
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
            for (int i = 0; i < 10; i++)
            {
                accountNumber += rnd.Next(10).ToString();
            }
            while (_accountRepository.GetByAccountNumber(accountNumber) != null)
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