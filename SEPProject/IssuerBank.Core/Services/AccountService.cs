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

        public Result<Account> UpdateBalance(Guid OwnerId, double amount, string currency)
        {
            Account account = _accountRepository.GetByUserId(OwnerId);
            if (account == null)
                return Result.Failure<Account>("Account does not exist.");
            amount = GetAmountBasedOnCurrency(amount, currency);
            account.IncreaseBalance(amount);
            _accountRepository.Edit(account);
            return Result.Success<Account>(account);
        }

        private static double GetAmountBasedOnCurrency(double amount, string currency)
        {
            return currency switch
            {
                "EUR" => amount,
                "USD" => amount / 1.13,
                "RSD" => amount / 117.57,
                "CAD" => amount / 1.43,
                _ => amount,
            };
        }
    }
}