using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Core.Model
{
    public class TransactionItem
    {
        public Guid Id { get; set; }
        public TransactionItemType Type { get; set; }
        public string Name { get; set; }
        public int Quantity{ get; set; }
        public double Price { get; set; }
        public Guid TransactionId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Transaction Transaction { get; set; }

        public TransactionItem()
        {
        }

        public TransactionItem(Guid id, TransactionItemType type, string name, int quantity, double price, Guid transactionId)
        {
            Id = id;
            Type = type;
            Name = name;
            Quantity = quantity;
            Price = price;
            TransactionId = transactionId;
        }
    }
}
