using System;

namespace WebShop.Core.DTOs
{
    public class EducationRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double price { get; set; }
        public bool online { get; set; }
        public string location { get; set; }
    }
}