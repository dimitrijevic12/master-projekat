namespace PCC.Api
{
    public static class Config
    {
        public static string IssuerBankServerAddress
        {
            get
            {
                return "https://localhost:44376/api/transactions";
            }
        }

        public static string AcquirerBankServerAddress
        {
            get
            {
                return "https://localhost:44375/api/transactions";
            }
        }
    }
}