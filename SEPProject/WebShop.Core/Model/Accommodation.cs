using System;

namespace WebShop.Core.Model
{
    public class Accommodation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CostPerNight { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public Guid OwnerId { get; set; }
        public virtual Admin Owner { get; set; }

        public Accommodation()
        {
        }

        public Accommodation(Guid id, string name, string description, 
            double costPerNight, string address, string imagePath, Guid ownerId)
        {
            Id = id;
            Name = name;
            Description = description;
            CostPerNight = costPerNight;
            Address = address;
            ImagePath = imagePath;
            OwnerId = ownerId;
        }
    }
}
