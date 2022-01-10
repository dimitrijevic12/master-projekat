using CSharpFunctionalExtensions;
using PCC.Core.Interface.Repository;
using PCC.Core.Interfaces.Repository;
using PCC.Core.Model;
using System;

namespace PCC.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankRepository _bankRepository;

        public TransactionService(ITransactionRepository transactionRepository, IBankRepository bankRepository)
        {
            _transactionRepository = transactionRepository;
            _bankRepository = bankRepository;
        }

        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan, string acquirerBankPan,
            TransactionStatus transactionStatus, Guid acquirerOrderId, DateTime acquirerTimestamp, Guid issuerOrderId, DateTime issuerTimestamp)
        {
            if (timestamp > DateTime.Now)
                return Result.Failure<Transaction>("Invalid timestamp.");
            Guid id = Guid.NewGuid();
            if (_transactionRepository.GetById(id) != null)
                return Result.Failure<Transaction>("Transaction with that id already exists.");
            var test = pan.Substring(0, 6);
            Bank issuerBank = _bankRepository.GetByPAN(test);
            if (issuerBank == null)
                return Result.Failure<Transaction>("Issuer bank with that pan does not exists.");
            Bank acquirerBank = _bankRepository.GetByPAN(acquirerBankPan);
            if (acquirerBank == null)
                return Result.Failure<Transaction>("Acquirer bank with that pan does not exists.");
            Transaction transaction = new Transaction(id, amount, currency, timestamp, paymentId, transactionStatus, acquirerOrderId, acquirerTimestamp,
                acquirerBank.Id, acquirerBank.Name, issuerBank.Id, issuerBank.Name, issuerOrderId, issuerTimestamp);
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }

        public Result<Transaction> Update(Transaction transaction)
        {
            if (_transactionRepository.GetById(transaction.Id) == null)
                return Result.Failure<Transaction>("Transaction with that Id doesn't exist.");
            _transactionRepository.Edit(transaction);
            return Result.Success<Transaction>(transaction);
        }
    }
}