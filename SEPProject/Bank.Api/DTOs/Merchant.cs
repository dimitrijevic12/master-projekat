using System;

namespace Bank.Api.DTOs
{
    public class Merchant
    {
        public Guid MerchantId { get; private set; }
        public string MerchantPassword { get; private set; }
        public string Name { get; private set; }

        public Merchant(Guid merchantId, string merchantPassword, string name)
        {
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Name = name;
        }
    }
}