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
                return "https://172.20.10.3:44390/api/transactions/status";
                //return "https://172.20.10.3:44300/api/Transactions/status";
            }
        }

        public static string PCCServerAddress
        {
            get
            {
                return "https://172.20.10.3:44320/api/transactions";
            }
        }

        public static string ClientAddress
        {
            get
            {
                return "https://172.20.10.3:44320/api/transactions";
            }
        }
    }
}