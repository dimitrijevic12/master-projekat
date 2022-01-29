using System;

namespace PCC.Api.DTOs
{
    public class PCCPerDiemRequest
    {
        public DateTime AcquirerTimestamp { get; set; }
        public Guid AcquirerTransactionId { get; set; }
        public Guid PaymentId { get; set; }
        public double Amount { get; set; }
        public string AcquirerAccountNumber { get; set; }
        public string Currency { get; set; }

        public PCCPerDiemRequest(DateTime acquirerTimestamp, Guid acquirerTransactionId, Guid paymentId, double amount,
            string acquirerAccountNumber, string currency)
        {
            AcquirerTimestamp = acquirerTimestamp;
            AcquirerTransactionId = acquirerTransactionId;
            PaymentId = paymentId;
            Amount = amount;
            AcquirerAccountNumber = acquirerAccountNumber;
            Currency = currency;
        }
    }
}