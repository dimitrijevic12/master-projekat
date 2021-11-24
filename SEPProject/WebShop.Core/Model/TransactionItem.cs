using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class TransactionItem
    {
        private Guid Id { get; set; }
        private TransactionItemType Type { get; set; }
        private string Name { get; set; }
        private int Quantity{ get; set; }
        private double Price { get; set; }

        public TransactionItem(Guid id, TransactionItemType type, string name, int quantity, double price)
        {
            Id = id;
            Type = type;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
