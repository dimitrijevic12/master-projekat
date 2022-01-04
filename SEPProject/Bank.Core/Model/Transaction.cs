using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public double Amount { get; private set; }
        public string Currency { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Guid PaymentId { get; private set; }
        public TransactionStatus TransactionStatus { get; private set; }
        public Guid AcquirerId { get; private set; }
        public string AcquirerName { get; private set; }
        public Guid IssuerId { get; private set; }
        public string IssuerName { get; private set; }

        public Transaction() : base()
        {
        }

        public Transaction(Guid id, double amount, string currency, DateTime timestamp, Guid paymentId, TransactionStatus transactionStatus,
            Guid acquirerId, string acquirerName, Guid issuerId, string issuerName)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
            Timestamp = timestamp;
            PaymentId = paymentId;
            TransactionStatus = transactionStatus;
            AcquirerId = acquirerId;
            AcquirerName = acquirerName;
            IssuerId = issuerId;
            IssuerName = issuerName;
        }
    }
}
