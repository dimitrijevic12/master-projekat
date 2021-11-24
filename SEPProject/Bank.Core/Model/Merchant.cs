using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class Merchant : User
    {

        private Guid MerchantId { get; set; }
        private string MerchantPassword { get; set; }

        public Merchant(Guid id, Guid merchantId, string merchantPassword) : base(id)
        {
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
        }
    }
}
