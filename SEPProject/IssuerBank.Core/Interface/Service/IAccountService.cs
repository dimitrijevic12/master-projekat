using CSharpFunctionalExtensions;
using IssuerBank.Core.Model;
using System;

namespace IssuerBank.Core.Interface.Service
{
    public interface IAccountService
    {
        public Result<Account> Create(Guid MerchantId);

        public Result<Account> UpdateBalance(Guid OwnerId, double amount, string currency);
    }
}