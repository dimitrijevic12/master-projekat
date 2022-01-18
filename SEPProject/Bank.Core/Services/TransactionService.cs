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
            Transaction transaction = null;
            Guid id = Guid.NewGuid();
            while (_transactionRepository.GetById(id) != null)
                id = Guid.NewGuid();
            PSPResponse pspResponse = _PSPResponseRepository.GetByPaymentId(paymentId);
            PSPRequest pspRequest = pspResponse.PSPRequest;
            Merchant acquirer = _merchantRepository.GetByMerchantId(pspRequest.MerchantId);
            PaymentCard card = _paymentCardRepository.GetByPAN(pan);
            if (card == null && pan.Substring(0, 6).Equals("123456"))
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Failed, acquirer.Id,
                acquirer.Name, Guid.Empty, "Unkwnown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            if (card == null)
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Pending, acquirer.Id,
                acquirer.Name, Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            if (amount < 0)
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Failed, acquirer.Id,
                acquirer.Name, card.CardOwnerId, card.CardOwner.FirstName + " " + card.CardOwner.LastName);
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            if (timestamp > DateTime.Now)
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Failed, acquirer.Id,
                acquirer.Name, card.CardOwnerId, card.CardOwner.FirstName + " " + card.CardOwner.LastName);
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            transaction = new Transaction(id, amount, currency, timestamp, paymentId, transactionStatus, acquirer.Id,
                acquirer.Name, card.CardOwnerId, card.CardOwner.FirstName + " " + card.CardOwner.LastName);
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