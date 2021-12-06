using CSharpFunctionalExtensions;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMerchantRepository _merchantRepository;

        public TransactionService(ITransactionRepository transactionRepository, IMerchantRepository merchantRepository)
        {
            _transactionRepository = transactionRepository;
            _merchantRepository = merchantRepository;
        }      

        public RequestDTO CreateTransactionForBank(TransactionDTO transactionDTO)
        {
            var merchant = _merchantRepository.GetByMerchantId(transactionDTO.MerchantId);
            return new RequestDTO(merchant.MerchantId, merchant.MerchantPassword, transactionDTO.Amount, transactionDTO.OrderId, transactionDTO.Timestamp,
                merchant.RegisteredWebShop.SuccessUrl, merchant.RegisteredWebShop.FailedUrl, merchant.RegisteredWebShop.ErrorUrl);
        }

        public Result Save(TransactionDTO transactionDTO)
        {
            if (_transactionRepository.GetTransactionByOrderId(transactionDTO.OrderId) != null) return Result.Failure("Transaction with that OrderId already exists!");
            if (transactionDTO.Amount <= 0) return Result.Failure("Amount is not valid!");
            if (_merchantRepository.GetByMerchantId(transactionDTO.MerchantId) == null) return Result.Failure("There is no Merchant with that MerchantId!");

            Transaction transaction = _transactionRepository.Save(new Transaction(transactionDTO.Id, transactionDTO.Amount, transactionDTO.Timestamp, transactionDTO.OrderId,
                transactionDTO.TransactionStatus, transactionDTO.MerchantId, transactionDTO.MerchantName, transactionDTO.IssuerId, transactionDTO.IssuerName));
            return Result.Success(transaction);
        }
    }
}
