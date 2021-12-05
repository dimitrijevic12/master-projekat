using CSharpFunctionalExtensions;
using System;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly IAdminRepository _adminRepository;

        public TransactionService(ITransactionRepository transactionRepository,
            IRegisteredUserRepository registeredUserRepository, 
            IAdminRepository adminRepository)
        {
            _transactionRepository = transactionRepository;
            _registeredUserRepository = registeredUserRepository;
            _adminRepository = adminRepository;
        }

        public Result Save(Transaction transaction)
        {
            if (String.IsNullOrEmpty(transaction.BuyerId.ToString()) ||
                String.IsNullOrEmpty(transaction.SellerId.ToString()))
            {
                return Result.Failure("Transaction must have buyer and seller!");
            }
            if (_registeredUserRepository.GetById(transaction.BuyerId) is null ||
                _adminRepository.GetById(transaction.SellerId) is null)
            {
                return Result.Failure("Seller or buyer doesn't exists!");
            }
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }

        public Result EditStatus(TransactionDTO transactionDTO)
        {
            Transaction transaction = _transactionRepository.GetById(transactionDTO.MerchantOrderId);
            bool result = Enum.TryParse(transactionDTO.TransactionStatus, out TransactionStatus transactionStatus);
            if (!result)
            {
                return Result.Failure("Inappropriate Transaction Status!");
            }
            transaction.Status = transactionStatus;
            _transactionRepository.Edit(transaction);
            return Result.Success(transaction);
        }
    }
}
