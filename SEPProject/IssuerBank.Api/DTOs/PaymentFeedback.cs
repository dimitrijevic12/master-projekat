using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssuerBank.Api.DTOs
{
    public class PaymentFeedback
    {
        public Guid MerchantOrderId { get; set; }
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public Guid IssuerOrderId { get; set; }
        public DateTime IssuerTimestamp { get; set; }
        public Guid PaymentId { get; set; }
    }
}
