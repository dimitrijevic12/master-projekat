using System;

namespace WebShop.Core.DTOs
{
    public class TransportationOption
    {
        public string name { get; set; }
        public double price { get; set; }
        public string startDestination { get; set; }
        public string finalDestination { get; set; }
        public DateTime departureTime { get; set; }
    }
}