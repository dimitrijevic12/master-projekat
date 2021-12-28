using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Core.DTOs
{
    public class TransactionStatusDTO
    {
        public Guid MerchantOrderId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
