using CardPayment.Core.DTOs;
using CardPayment.Core.Interface.Repository;
using CardPayment.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.Services
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
                merchant.RegisteredWebShop.SuccessUrl, merchant.RegisteredWebShop.FailedUrl, merchant.RegisteredWebShop.ErrorUrl);
        }

        public Transaction EditTransaction(TransactionStatusDTO transactionStatusDTO)
        {
            var transaction = _transactionRepository.GetTransactionByOrderId(transactionStatusDTO.MerchantOrderId);
            if (transaction == null) return null;
            else return EditStatus(transaction, transactionStatusDTO.TransactionStatus);
        }

        private Transaction EditStatus(Transaction transaction, string status)
        {
            if (status.Equals("Success")) transaction.TransactionStatus = TransactionStatus.Success;
            else if (status.Equals("Failed")) transaction.TransactionStatus = TransactionStatus.Failed;
            else transaction.TransactionStatus = TransactionStatus.Error;
            return _transactionRepository.Edit(transaction);
        }
    }
}
