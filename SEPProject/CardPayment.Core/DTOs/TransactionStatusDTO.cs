using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.DTOs
{
    public class TransactionStatusDTO
    {
        public Guid MerchantOrderId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
