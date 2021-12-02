using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.DTOs
{
    public class PaymentTypeRegisteredWebShopDTO
    {
        public Guid RegisteredWebShopId { get; set; }
        public Tuple<Guid, bool> PayPal { get; set; }
        public Tuple<Guid, bool> CryptoValute { get; set; }
        public Tuple<Guid, bool> Bank { get; set; }
    }
}
