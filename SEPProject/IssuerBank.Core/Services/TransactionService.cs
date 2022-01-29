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
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IPSPResponseRepository pSPResponseRepository,
            IMerchantRepository merchantRepository, IRegisteredUserRepository registeredUserRepository,
            IPaymentCardRepository paymentCardRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _PSPResponseRepository = pSPResponseRepository;
            _merchantRepository = merchantRepository;
            _registeredUserRepository = registeredUserRepository;
            _paymentCardRepository = paymentCardRepository;
            _accountRepository = accountRepository;
        }

        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan, TransactionStatus transactionStatus)
        {
            Transaction transaction = null;
            Guid id = Guid.NewGuid();
            while (_transactionRepository.GetById(id) != null)
                id = Guid.NewGuid();
            PaymentCard card = _paymentCardRepository.GetByPAN(pan);
            if (card == null)
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Error, Guid.Empty, "Unknown",
                Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            RegisteredUser issuer = _registeredUserRepository.GetById(card.CardOwnerId);
            if (amount < 0)
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Failed, Guid.Empty, "Unknown",
                issuer.Id, issuer.FirstName + " " + issuer.LastName);
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            if (timestamp > DateTime.Now)
            {
                transaction = new Transaction(id, amount, currency, timestamp, paymentId, TransactionStatus.Failed, Guid.Empty, "Unknown",
                issuer.Id, issuer.FirstName + " " + issuer.LastName);
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            transaction = new Transaction(id, amount, currency, timestamp, paymentId, transactionStatus, Guid.Empty, "Unknown",
                issuer.Id, issuer.FirstName + " " + issuer.LastName);
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }

        public Result<Transaction> CreatePerDiem(string uniquePersonalRegistrationNumber, double amount, string currency, Guid transactionId)
        {
            Transaction transaction = null;
            Guid id = Guid.NewGuid();
            while (_transactionRepository.GetById(id) != null)
                id = Guid.NewGuid();
            if (_transactionRepository.GetByPaymentId(transactionId) != null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, transactionId, TransactionStatus.Error, Guid.Empty,
                "Unknown", Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            RegisteredUser registeredUser = _registeredUserRepository.GetByUniquePersonalRegistrationNumber(uniquePersonalRegistrationNumber);
            if (registeredUser == null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, transactionId, TransactionStatus.Failed, Guid.Empty,
                "Unknown", Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            Account account = _accountRepository.GetByUserId(registeredUser.Id);
            if (account == null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, transactionId, TransactionStatus.Failed, registeredUser.Id,
                registeredUser.FirstName + " " + registeredUser.LastName, Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            transaction = new Transaction(id, amount, currency, DateTime.Now, transactionId, TransactionStatus.Pending, registeredUser.Id,
                registeredUser.FirstName + " " + registeredUser.LastName, Guid.Empty, "Unknown");
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }
    }
}