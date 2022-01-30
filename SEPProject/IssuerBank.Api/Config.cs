namespace IssuerBank.Api
{
    public static class Config
    {
        public static string PCCServerAddress
        {
            get
            {
                return "https://192.168.1.62:44320/api/transactions/per-diem";
            }
        }

        public static string WebShopAddress
        {
            get
            {
                return "https://192.168.1.18:44326/api/transactions/per-diem";
            }
        }
    }
}