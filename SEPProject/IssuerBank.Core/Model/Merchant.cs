using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Model
{
    public class Merchant : User
    {

        public Guid MerchantId { get; private set; }
        public string MerchantPassword { get; private set; }
        public string Name { get; private set; }

        public Merchant() : base()
        {
        }

        public Merchant(Guid id, Guid merchantId, string merchantPassword, string name) : base(id)
        {
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Name = name;
        }
    }
}
