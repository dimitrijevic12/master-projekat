using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;

namespace Bank.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPSPResponseRepository _PSPResponseRepository;
        private readonly IMerchantRepository _merchantRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly IPaymentCardRepository _paymentCardRepository;

        public TransactionService(ITransactionRepository transactionRepository, IPSPResponseRepository pSPResponseRepository,
            IMerchantRepository merchantRepository, IRegisteredUserRepository registeredUserRepository,
            IPaymentCardRepository paymentCardRepository)
        {
            _transactionRepository = transactionRepository;
            _PSPResponseRepository = pSPResponseRepository;
            _merchantRepository = merchantRepository;
            _registeredUserRepository = registeredUserRepository;
            _paymentCardRepository = paymentCardRepository;
        }

        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan, TransactionStatus transactionStatus)
        {
            if (amount < 0)
                return Result.Failure<Transaction>("Amount can not be negative number.");
            if (timestamp > DateTime.Now)
                return Result.Failure<Transaction>("Invalid timestamp.");
            PSPResponse pspResponse = _PSPResponseRepository.GetByPaymentId(paymentId);
            PSPRequest pspRequest = pspResponse.PSPRequest;
            Guid id = Guid.NewGuid();
            if (_transactionRepository.GetById(id) != null)
                return Result.Failure<Transaction>("Transaction with that id already exists.");
            Merchant acquirer = _merchantRepository.GetByMerchantId(pspRequest.MerchantId);
            Transaction transaction = new Transaction(id, amount, currency, timestamp, paymentId, transactionStatus, acquirer.Id,
                acquirer.Name, Guid.Empty, "Unknown");
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }

        public Result<Transaction> Update(Transaction transaction)
        {
            if (_transactionRepository.GetById(transaction.Id) == null) return Result.Failure<Transaction>("Transaction with that id does not exist");
            _transactionRepository.Edit(transaction);
            return Result.Success(transaction);
        }
    }
}