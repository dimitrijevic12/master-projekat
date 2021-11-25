using System;

namespace WebShop.Core.Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Online { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public Guid OwnerId { get; set; }
        public virtual Admin Owner { get; set; }

        public Course()
        {
        }

        public Course(Guid id, string name, string description, bool online, DateTime startDate, DateTime endDate,
            double price, string address, string imagePath, Guid ownerId)
        {
            Id = id;
            Name = name;
            Description = description;
            Online = online;
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
            Address = address;
            ImagePath = imagePath;
            OwnerId = ownerId;
        }
    }
}
