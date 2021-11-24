using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class PSPRequest
    {
        private Guid MerchantId { get; set; }
        private string MerchantPassword{ get; set; }
        private double Amount{ get; set; }
        private Guid MerchantOrderId { get; set; }
        private DateTime MerchantTimestamp { get; set; }
        private Uri SuccessUrl{ get; set; }
        private Uri FailedUrl { get; set; }
        private Uri ErrorUrl { get; set; }

        public PSPRequest(Guid merchantId, string merchantPassword, double amount, Guid merchantOrderId, DateTime merchantTimestamp,
            Uri successUrl, Uri failedUrl, Uri errorUrl)
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
