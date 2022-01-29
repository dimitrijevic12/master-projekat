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

        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan,
            TransactionStatus transactionStatus)
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

        public Result<Transaction> CreatePerDiem(string uniquePersonalRegistrationNumber, double amount, string currency, string accountNumber)
        {
            Transaction transaction = null;
            Guid id = Guid.NewGuid();
            while (_transactionRepository.GetById(id) != null)
                id = Guid.NewGuid();
            Guid paymentId = Guid.NewGuid();
            while (_transactionRepository.GetByPaymentId(paymentId) != null)
                paymentId = Guid.NewGuid();
            RegisteredUser registeredUser = _registeredUserRepository.GetByUniquePersonalRegistrationNumber(uniquePersonalRegistrationNumber);
            if (registeredUser == null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, paymentId, TransactionStatus.Failed, Guid.Empty,
                "Unknown", Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            Account issuerAccount = _accountRepository.GetByAccountNumber(accountNumber);
            if (issuerAccount == null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, paymentId, TransactionStatus.Success, registeredUser.Id,
                registeredUser.FirstName + " " + registeredUser.LastName, Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            RegisteredUser issuer = _registeredUserRepository.GetById(issuerAccount.UserId);
            Account account = _accountRepository.GetByUserId(registeredUser.Id);
            if (account == null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, paymentId, TransactionStatus.Failed, registeredUser.Id,
                registeredUser.FirstName + " " + registeredUser.LastName, issuerAccount.UserId, issuer.FirstName + " " + issuer.LastName);
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            transaction = new Transaction(id, amount, currency, DateTime.Now, paymentId, TransactionStatus.Success, registeredUser.Id,
                registeredUser.FirstName + " " + registeredUser.LastName, issuerAccount.UserId, issuer.FirstName + " " + issuer.LastName);
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }

        public Result<Transaction> CreatePerDiemFromPCC(double amount, string currency, string accountNumber)
        {
            Transaction transaction = null;
            Guid id = Guid.NewGuid();
            while (_transactionRepository.GetById(id) != null)
                id = Guid.NewGuid();
            Guid paymentId = Guid.NewGuid();
            while (_transactionRepository.GetByPaymentId(paymentId) != null)
                paymentId = Guid.NewGuid();
            Account issuerAccount = _accountRepository.GetByAccountNumber(accountNumber);
            if (issuerAccount == null)
            {
                transaction = new Transaction(id, amount, currency, DateTime.Now, paymentId, TransactionStatus.Success, Guid.Empty,
                "Unkwnown", Guid.Empty, "Unknown");
                _transactionRepository.Save(transaction);
                return Result.Success<Transaction>(transaction);
            }
            RegisteredUser issuer = _registeredUserRepository.GetById(issuerAccount.UserId);
            transaction = new Transaction(id, amount, currency, DateTime.Now, paymentId, TransactionStatus.Success, Guid.Empty,
                "Unkwnown", issuerAccount.UserId, issuer.FirstName + " " + issuer.LastName);
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