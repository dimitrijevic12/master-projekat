using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.DTOs
{
    public class RequestDTO
    {
        public Guid MerchantId { get; set; }
        public string MerchantPassword { get; set; }
        public double Amount { get; set; }
        public Guid MerchantOrderId { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public Uri SuccessUrl { get; set; }
        public Uri FailedUrl { get; set; }
        public Uri ErrorUrl { get; set; }
        public RequestDTO(Guid merchantId, string merchantPassword, double amount, Guid merchantOrderId, DateTime merchantTimestamp, Uri successUrl, Uri failedUrl, Uri errorUrl)
        {
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Amount = amount;
            MerchantOrderId = merchantOrderId;
            MerchantTimestamp = merchantTimestamp;
            SuccessUrl = successUrl;
            FailedUrl = failedUrl;
            ErrorUrl = errorUrl;
        }
    }
}
