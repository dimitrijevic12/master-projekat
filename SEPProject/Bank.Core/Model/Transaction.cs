using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class Transaction
    {
        private Guid Id { get; set; }
        private double Amount { get; set; }
        private DateTime Timestamp { get; set; }
        private Guid OrderId { get; set; }
        private TransactionStatus TransactionStatus { get; set; }
        private Guid AcquirerId { get; set; }
        private string AcquirerName { get; set; }
        private Guid IssuerId { get; set; }
        private string IssuerName { get; set; }

        public Transaction(Guid id, double amount, DateTime timestamp, Guid orderId, TransactionStatus transactionStatus,
            Guid acquirerId, string acquirerName, Guid issuerId, string issuerName)
        {
            Id = id;
            Amount = amount;
            Timestamp = timestamp;
            OrderId = orderId;
            TransactionStatus = transactionStatus;
            AcquirerId = acquirerId;
            AcquirerName = acquirerName;
            IssuerId = issuerId;
            IssuerName = issuerName;
        }
    }
}
