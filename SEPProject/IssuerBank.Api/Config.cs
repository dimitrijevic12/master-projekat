namespace IssuerBank.Api
{
    public static class Config
    {
        public static string PCCServerAddress
        {
            get
            {
                return "https://172.20.10.3:44320/api/transactions/per-diem";
            }
        }

        public static string WebShopAddress
        {
            get
            {
                return "https://172.20.10.2:44326/api/transactions/per-diem";
            }
        }
    }
}