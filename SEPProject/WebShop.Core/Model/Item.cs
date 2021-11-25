using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Item
    {
        [Key]
        public Guid ProductKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public Guid OwnerId { get; set; }
        public virtual Admin Owner { get; set; }

        public Item()
        {
        }

        public Item(Guid productKey, string name, string description, double price, string imagePath, Guid ownerId)
        {
            ProductKey = productKey;
            Name = name;
            Description = description;
            Price = price;
            ImagePath = imagePath;
            OwnerId = ownerId;
        }
    }
}
