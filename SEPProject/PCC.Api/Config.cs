namespace PCC.Api
{
    public static class Config
    {
        public static string IssuerBankServerAddress
        {
            get
            {
                return "https://172.20.10.3:44376/api/transactions";
            }
        }

        public static string AcquirerBankServerAddress
        {
            get
            {
                return "https://172.20.10.3:44375/api/transactions";
            }
        }
    }
}