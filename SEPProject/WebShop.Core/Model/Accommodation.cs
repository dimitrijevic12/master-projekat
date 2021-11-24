using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Accommodation
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private double CostPerNight { get; set; }
        private string Address { get; set; }
        private string ImagePath { get; set; }
        public virtual Admin Owner { get; set; }

        public Accommodation(Guid id, string name, string description, double costPerNight, string address, string imagePath, Admin owner)
        {
            Id = id;
            Name = name;
            Description = description;
            CostPerNight = costPerNight;
            Address = address;
            ImagePath = imagePath;
            Owner = owner;
        }
    }
}
