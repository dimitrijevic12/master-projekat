using CryptoValute.Core.DTOs;
using CryptoValute.Core.Interface.Repository;
using CryptoValute.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.Core.Services
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

        public Transaction SetTransactionsPaymentId(TransactionsPaymentIdDTO transactionsPaymentIdDTO)
        {
            var transaction = _transactionRepository.GetTransactionByOrderId(transactionsPaymentIdDTO.OrderId);
            if (transaction == null) return null;
            transaction.PaymentId = transactionsPaymentIdDTO.PaymentId;
            return _transactionRepository.Edit(transaction);
        }

        public RequestDTO CreateRequestForBank(Guid orderId)
        {
            var transaction = _transactionRepository.GetTransactionByOrderId(orderId);
            if (transaction == null) return null;
            var merchant = _merchantRepository.GetByMerchantId(transaction.MerchantId);
            return new RequestDTO(merchant.MerchantId, merchant.MerchantPassword, transaction.Amount, transaction.OrderId, transaction.Timestamp,
                new Uri(merchant.RegisteredWebShop.SuccessUrl + "/" + orderId), new Uri(merchant.RegisteredWebShop.FailedUrl + "/" + orderId), new Uri(merchant.RegisteredWebShop.ErrorUrl + "/" + orderId));
        }

        public Transaction EditTransaction(TransactionStatusDTO transactionStatusDTO)
        {
            var transaction = _transactionRepository.GetTransactionByOrderId(transactionStatusDTO.MerchantOrderId);
            if (transaction == null) return null;
            else return EditStatus(transaction, transactionStatusDTO.TransactionStatus);
        }

        private Transaction EditStatus(Transaction transaction, string status)
        {
            bool result = Enum.TryParse(status, out TransactionStatus transactionStatus);
            if (!result)
            {
                return null;
            }
            transaction.TransactionStatus = transactionStatus;
            return _transactionRepository.Edit(transaction);
        }
    }
}
