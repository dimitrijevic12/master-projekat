using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class Merchant : User
    {

        public Guid MerchantId { get; private set; }
        public string MerchantPassword { get; private set; }

        public Merchant() : base()
        {
        }

        public Merchant(Guid id, Guid merchantId, string merchantPassword) : base(id)
        {
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
        }
    }
}
