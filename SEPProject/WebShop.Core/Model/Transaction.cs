using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Transaction
    {
        private Guid Id { get; set; }
        private TransactionStatus Status { get; set; }
        private DateTime Timestamp { get; set; }
        private double TotalPrice { get; set; }
        public virtual Admin Seller { get; set; }
        public virtual RegisteredUser Buyer { get; set; }
        public virtual IEnumerable<TransactionItem> TransactionItems { get; set; }

        public Transaction(Guid id, TransactionStatus status, DateTime timestamp, double totalPrice, Admin seller, RegisteredUser buyer,
            IEnumerable<TransactionItem> transactionItems)
        {
            Id = id;
            Status = status;
            Timestamp = timestamp;
            TotalPrice = totalPrice;
            Seller = seller;
            Buyer = buyer;
            TransactionItems = transactionItems;
        }
    }
}
