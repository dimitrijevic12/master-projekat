﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Core.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
        public double TotalPrice { get; set; }
        public Guid SellerId { get; set; }
        public virtual Admin Seller { get; set; }
        public Guid BuyerId { get; set; }
        public virtual RegisteredUser Buyer { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }

        public Transaction()
        {
        }

        public Transaction(Guid id, TransactionStatus status, DateTime timestamp, double totalPrice, Guid sellerId, Guid buyerId)
        {
            Id = id;
            Status = status;
            Timestamp = timestamp;
            TotalPrice = totalPrice;
            SellerId = sellerId;
            BuyerId = buyerId;
        }
    }
}
