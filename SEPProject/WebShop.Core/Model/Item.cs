using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Item
    {
        private Guid ProductKey { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private double Price { get; set; }
        private string ImagePath { get; set; }
        public virtual Admin Owner { get; set; }

        public Item(Guid productKey, string name, string description, double price, string imagePath, Admin owner)
        {
            ProductKey = productKey;
            Name = name;
            Description = description;
            Price = price;
            ImagePath = imagePath;
            Owner = owner;
        }
    }
}
