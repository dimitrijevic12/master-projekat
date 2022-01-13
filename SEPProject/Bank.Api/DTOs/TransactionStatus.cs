namespace Bank.Api.DTOs
{
    public class TransactionStatus
    {
        public string Status { get; set; }

        public TransactionStatus(string status)
        {
            Status = status;
        }
    }
}