using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;

namespace Bank.Core.Interface.Service
{
    public interface ITransactionService
    {
        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan, TransactionStatus transactionStatus);

        public Result<Transaction> CreatePerDiem(string uniquePersonalRegistrationNumber, double amount, string currency);
    }
}