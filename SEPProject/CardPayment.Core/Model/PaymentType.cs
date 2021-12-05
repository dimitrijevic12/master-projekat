using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.Model
{
    public class PaymentType
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<RegisteredWebShop> RegisteredWebShops { get; private set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<PaymentTypeRegisteredWebShop> PaymentTypeRegisteredWebShops { get; private set; }

        public PaymentType()
        {
        }

        public PaymentType(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
