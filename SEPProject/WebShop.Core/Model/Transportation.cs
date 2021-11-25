using System;

namespace WebShop.Core.Model
{
    public class Transportation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string StartDestination { get; set; }
        public string FinalDestination { get; set; }
        public DateTime DepartureTime { get; set; }
        public string ImagePath { get; set; }
        public Guid OwnerId { get; set; }
        public virtual Admin Owner { get; set; }

        public Transportation()
        {
        }

        public Transportation(Guid id, string name, string description, 
            double price, string startDestination, string finalDestination, 
            DateTime departureTime, string imagePath, Guid ownerId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StartDestination = startDestination;
            FinalDestination = finalDestination;
            DepartureTime = departureTime;
            ImagePath = imagePath;
            OwnerId = ownerId;
        }
    }
}
