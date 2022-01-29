using System;

namespace Bank.Api.DTOs
{
    public class BankPerDiemRequest
    {
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public double Amount { get; set; }
        public string IssuerAccountNumber { get; set; }
        public string Currency { get; set; }

        public BankPerDiemRequest(Guid acquirerOrderId, DateTime acquirerTimestamp, double amount, string issuerAccountNumber, string currency)
        {
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            Amount = amount;
            IssuerAccountNumber = issuerAccountNumber;
            Currency = currency;
        }
    }
}