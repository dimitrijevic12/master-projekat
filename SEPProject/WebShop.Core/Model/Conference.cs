using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Conference
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private bool Online { get; set; }
        private double Price { get; set; }
        private string Address { get; set; }
        private string ImagePath{ get; set; }
        public virtual Admin Owner { get; set; }

        public Conference(Guid id, string name, string description, bool online, double price, string address, string imagePath, Admin owner)
        {
            Id = id;
            Name = name;
            Description = description;
            Online = online;
            Price = price;
            Address = address;
            ImagePath = imagePath;
            Owner = owner;
        }
    }
}
