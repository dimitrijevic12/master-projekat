namespace Bank.Api
{
    public static class Config
    {
        public static string BankPan
        {
            get
            {
                return "123456";
            }
        }

        public static string PSPServerAddress
        {
            get
            {
                return "https://localhost:44390/api/transactions/status";
            }
        }

        public static string PCCServerAddress
        {
            get
            {
                return "https://localhost:44320/api/transactions";
            }
        }

        public static string ClientAddress
        {
            get
            {
                return "https://localhost:44320/api/transactions";
            }
        }
    }
}