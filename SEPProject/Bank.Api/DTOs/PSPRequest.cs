using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.DTOs
{
    public class PSPRequest
    {
        public Guid MerchantId { get; set; }
        public string MerchantPassword{ get; set; }
        public double Amount{ get; set; }
        public string Currency { get; set; }
        public Guid MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public Uri SuccessUrl { get; set; }
        public Uri FailedUrl { get; set; }
        public Uri ErrorUrl { get; set; }
    }
}
