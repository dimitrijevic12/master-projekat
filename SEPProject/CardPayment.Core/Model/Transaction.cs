using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.Model
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public double Amount { get; private set; }
        public string Currency { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Guid OrderId { get; private set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Guid MerchantId { get; private set; }
        public string MerchantName { get; private set; }
        public Guid IssuerId { get; private set; }
        public string IssuerName { get; private set; }
        public Guid PaymentId { get; set; }
        public string Type { get; private set; }

        public Transaction()
        {
        }

        public Transaction(Guid id, double amount, string currency, DateTime timestamp, Guid orderId, TransactionStatus transactionStatus,
            Guid merchantId, string merchantName, Guid issuerId, string issuerName, string type)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
            Timestamp = timestamp;
            OrderId = orderId;
            TransactionStatus = transactionStatus;
            MerchantId = merchantId;
            MerchantName = merchantName;
            IssuerId = issuerId;
            IssuerName = issuerName;
            Type = type;
        }
    }
}
