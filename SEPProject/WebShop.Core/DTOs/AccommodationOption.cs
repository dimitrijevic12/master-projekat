using System;

namespace WebShop.Core.DTOs
{
    public class AccommodationOption
    {
        public string name { get; set; }
        public double costPerNight { get; set; }
        public string location { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}