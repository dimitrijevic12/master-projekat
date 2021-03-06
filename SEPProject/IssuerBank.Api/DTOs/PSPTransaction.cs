using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssuerBank.Api.DTOs
{
    public class PSPTransaction
    {
        public Guid MerchantOrderId { get; set; }
        public string TransactionStatus { get; set; }
        public PSPTransaction(Guid merchantOrderId, string transactionStatus)
        {
            MerchantOrderId = merchantOrderId;
            TransactionStatus = transactionStatus;
        }
    }
}
