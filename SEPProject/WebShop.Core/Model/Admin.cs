using System;

namespace WebShop.Core.Model
{
    public class Admin : User
    {
        public string Name { get; set; }
        public Guid MerchantId { get; set; }

        public Admin() : base ()
        {
        }

        public Admin(Guid id, string email, string username, string password, string name, Guid merchantId) :
            base(id, email, username, password)
        {
            Name = name;
            MerchantId = merchantId;
        }
    }
}
