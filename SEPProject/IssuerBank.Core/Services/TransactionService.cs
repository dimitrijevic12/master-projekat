using CSharpFunctionalExtensions;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using System;

namespace IssuerBank.Core.Services
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
            Guid id = Guid.NewGuid();
            if (_transactionRepository.GetById(id) != null)
                return Result.Failure<Transaction>("Transaction with that id already exists.");
            PaymentCard card = _paymentCardRepository.GetByPAN(pan);
            if (card == null)
                return Result.Failure<Transaction>("Invalid PAN.");
            RegisteredUser issuer = _registeredUserRepository.GetById(card.CardOwnerId);
            Transaction transaction = new Transaction(id, amount, currency, timestamp, paymentId, transactionStatus, Guid.Empty, "Unknown",
                issuer.Id, issuer.FirstName + " " + issuer.LastName);
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }
    }
}