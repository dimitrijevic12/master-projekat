using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.Model
{
    public class Merchant
    {
        private Guid Id { get; set; }
        private Guid MerchantId {get; set;}
        private string MerchantPassword { get; set; }
        private string Name { get; set; }

        public Merchant(Guid id, Guid merchantId, string merchantPassword, string name)
        {
            Id = id;
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Name = name;
        }
    }
}
