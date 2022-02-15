namespace WebShop.Core.DTOs
{
    public class EducationPurchaseOrder
    {
        public string educationRequest { get; set; }
        public string accommodationOption { get; set; }
        public string transportationOption { get; set; }
        public bool online { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}