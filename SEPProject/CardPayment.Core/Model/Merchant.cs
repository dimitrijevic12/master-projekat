using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.Model
{
    public class Merchant
    {
        public Guid Id { get; private set; }
        public Guid MerchantId { get; private set; }
        public string MerchantPassword { get; private set; }
        public string Name { get; private set; }
        public Guid RegisteredWebShopId { get; private set; }
        public virtual RegisteredWebShop RegisteredWebShop { get; private set; }

        public Merchant(Guid id, Guid merchantId, string merchantPassword, string name, Guid registeredWebShopId)
        {
            Id = id;
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Name = name;
            RegisteredWebShopId = registeredWebShopId;
        }
    }
}