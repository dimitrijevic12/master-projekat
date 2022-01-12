using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.Core.Model
{
    public class PaymentTypeRegisteredWebShop
    {
        public Guid PaymentTypeId { get; private set; }
        public virtual PaymentType PaymentType { get; private set; }
        public Guid RegisteredWebShopId { get; private set; }
        public virtual RegisteredWebShop RegisteredWebShop { get; private set; }

        public PaymentTypeRegisteredWebShop()
        {
        }

        public PaymentTypeRegisteredWebShop(Guid paymentTypeId, Guid registeredWebShopId)
        {
            PaymentTypeId = paymentTypeId;
            RegisteredWebShopId = registeredWebShopId;
        }
    }
}
