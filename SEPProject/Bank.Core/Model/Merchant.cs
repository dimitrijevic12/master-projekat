using System;

namespace Bank.Core.Model
{
    public class Merchant : User
    {
        public Guid MerchantId { get; private set; }
        public string MerchantPassword { get; private set; }
        public string Name { get; private set; }
        public string Salt { get; private set; }

        public Merchant() : base()
        {
        }

        public Merchant(Guid id, Guid merchantId, string merchantPassword, string name, string salt) : base(id)
        {
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Name = name;
            Salt = salt;
        }
    }
}