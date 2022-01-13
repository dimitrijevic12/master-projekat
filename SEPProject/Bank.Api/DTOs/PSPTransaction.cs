using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.DTOs
{
    public class PSPTransaction
    {
        public Guid MerchantOrderId { get; set; }
        public string TransactionStatus { get; set; }
        public Guid TransactionId { get; set; }

        public PSPTransaction(Guid merchantOrderId, string transactionStatus, Guid transactionId)
        {
            MerchantOrderId = merchantOrderId;
            TransactionStatus = transactionStatus;
            TransactionId = transactionId;
        }
    }
}
