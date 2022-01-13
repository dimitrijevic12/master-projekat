using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;

namespace Bank.Core.Interface.Service
{
    public interface IAccountService
    {
        public Result<Account> Create(Guid MerchantId);

        public Result<Account> UpdateBalance(Guid OwnerId, double amount, string currency);
    }
}