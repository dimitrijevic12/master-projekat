namespace Bank.Api.DTOs
{
    public class PerDiem
    {
        public string AccountNumber { get; set; }
        public string UniquePersonalRegistrationNumber { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}