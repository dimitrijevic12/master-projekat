using System;

namespace WebShop.Core.Model
{
    public class Conference
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Online { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public string ImagePath{ get; set; }
        public Guid OwnerId { get; set; }
        public virtual Admin Owner { get; set; }

        public Conference()
        {
        }

        public Conference(Guid id, string name, string description, bool online, 
            double price, string address, string imagePath, Guid ownerId)
        {
            Id = id;
            Name = name;
            Description = description;
            Online = online;
            Price = price;
            Address = address;
            ImagePath = imagePath;
            OwnerId = ownerId;
        }
    }
}
