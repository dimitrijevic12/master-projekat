using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class PSPRequest
    {
        public Guid Id { get; private set; }
        public Guid MerchantId { get; private set; }
        public string MerchantPassword{ get; private set; }
        public double Amount{ get; set; }
        public string Currency { get; set; }
        public Guid MerchantOrderId { get; private set; }
        public DateTime MerchantTimestamp { get; private set; }
        public Uri SuccessUrl{ get; private set; }
        public Uri FailedUrl { get; private set; }
        public Uri ErrorUrl { get; private set; }

        public PSPRequest() : base()
        {
        }

        public PSPRequest(Guid id, Guid merchantId, string merchantPassword, double amount, string currency, Guid merchantOrderId, DateTime merchantTimestamp,
            Uri successUrl, Uri failedUrl, Uri errorUrl)
        {
            Id = id;
            MerchantId = merchantId;
            MerchantPassword = merchantPassword;
            Amount = amount;
            Currency = currency;
            MerchantOrderId = merchantOrderId;
            MerchantTimestamp = merchantTimestamp;
            SuccessUrl = successUrl;
            FailedUrl = failedUrl;
            ErrorUrl = errorUrl;
        }
    }
}
