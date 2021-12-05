using System;

namespace WebShop.Core.DTOs
{
    public class TransactionDTO
    {
        public Guid MerchantOrderId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
