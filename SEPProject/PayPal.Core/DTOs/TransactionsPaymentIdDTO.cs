using System;

namespace PayPal.Core.DTOs
{
    public class TransactionsPaymentIdDTO
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
