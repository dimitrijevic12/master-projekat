using System;

namespace WebShop.Core.DTOs
{
    public class TransactionDTO
    {
        public Guid TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
