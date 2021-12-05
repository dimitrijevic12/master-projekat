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
    }
}
