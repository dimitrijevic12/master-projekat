using System;

namespace PCC.Core.Model
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public double Amount { get; private set; }
        public string Currency { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Guid PaymentId { get; private set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Guid AcquirerOrderId { get; private set; }
        public DateTime AcquirerTimestamp { get; private set; }
        public Guid AcquirerBankId { get; private set; }
        public string AcquirerBankName { get; private set; }
        public Guid IssuerBankId { get; private set; }
        public string IssuerBankName { get; private set; }
        public Guid IssuerOrderId { get; set; }
        public DateTime IssuerTimestamp { get; set; }

        public Transaction(Guid id, double amount, string currency, DateTime timestamp, Guid paymentId, TransactionStatus transactionStatus,
            Guid acquirerOrderId, DateTime acquirerTimestamp, Guid acquirerBankId, string acquirerBankName, Guid issuerBankId, string issuerBankName,
            Guid issuerOrderId, DateTime issuerTimestamp)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
            Timestamp = timestamp;
            PaymentId = paymentId;
            TransactionStatus = transactionStatus;
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            AcquirerBankId = acquirerBankId;
            AcquirerBankName = acquirerBankName;
            IssuerBankId = issuerBankId;
            IssuerBankName = issuerBankName;
            IssuerOrderId = issuerOrderId;
            IssuerTimestamp = issuerTimestamp;
        }
    }
}