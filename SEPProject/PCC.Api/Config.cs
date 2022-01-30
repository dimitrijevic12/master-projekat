namespace PCC.Api
{
    public static class Config
    {
        public static string IssuerBankServerAddress
        {
            get
            {
                return "https://192.168.1.62:44376/api/transactions";
            }
        }

        public static string AcquirerBankServerAddress
        {
            get
            {
                return "https://192.168.1.62:44375/api/transactions";
            }
        }
    }
}