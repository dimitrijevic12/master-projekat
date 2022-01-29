namespace IssuerBank.Api
{
    public static class Config
    {
        public static string PCCServerAddress
        {
            get
            {
                return "https://localhost:44320/api/transactions/per-diem";
            }
        }

        public static string WebShopAddress
        {
            get
            {
                return "https://localhost:44326/api/transactions/per-diem";
            }
        }
    }
}