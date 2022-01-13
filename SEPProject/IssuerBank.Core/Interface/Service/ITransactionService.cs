using CSharpFunctionalExtensions;
using IssuerBank.Core.Model;
using System;

namespace IssuerBank.Core.Interface.Service
{
    public interface ITransactionService
    {
        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan, TransactionStatus transactionStatus);
    }
}