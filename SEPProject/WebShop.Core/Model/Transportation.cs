using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Transportation
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private double Price { get; set; }
        private string StartDestination { get; set; }
        private string FinalDestination { get; set; }
        private string ImagePath { get; set; }
        public virtual Admin Owner { get; set; }

        public Transportation(Guid id, string name, string description, double price, string startDestination, string finalDestination, string imagePath, Admin owner)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StartDestination = startDestination;
            FinalDestination = finalDestination;
            ImagePath = imagePath;
            Owner = owner;
        }
    }
}
