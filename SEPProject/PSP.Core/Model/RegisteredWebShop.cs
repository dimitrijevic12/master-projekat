using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Model
{
    public class RegisteredWebShop
    {
        public Guid Id { get; private set; }
        public int WebShopId { get; private set; }
        public string WebShopName { get; private set; }
        public string Password { get; private set; }
        public string EmailAddress { get; private set; }
        public Uri SuccessUrl { get; private set; }
        public Uri FailedUrl { get; private set; }
        public Uri ErrorUrl { get; private set; }

        public virtual ICollection<PaymentType> PaymentTypes { get; private set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<PaymentTypeRegisteredWebShop> PaymentTypeRegisteredWebShops { get; private set; }

        public RegisteredWebShop()
        {
        }

        public RegisteredWebShop(Guid id, int webShopId, string webShopName, string password, string emailAddress,
            Uri successUrl, Uri failedUrl, Uri errorUrl)
        {
            Id = id;
            WebShopId = webShopId;
            WebShopName = webShopName;
            Password = password;
            EmailAddress = emailAddress;
            SuccessUrl = successUrl;
            FailedUrl = failedUrl;
            ErrorUrl = errorUrl;
        }

        public RegisteredWebShop(Guid id, int webShopId, string webShopName, string password, string emailAddress,
            Uri successUrl, Uri failedUrl, Uri errorUrl, List<PaymentType> paymentTypes)
        {
            Id = id;
            WebShopId = webShopId;
            WebShopName = webShopName;
            Password = password;
            EmailAddress = emailAddress;
            SuccessUrl = successUrl;
            FailedUrl = failedUrl;
            ErrorUrl = errorUrl;
            PaymentTypes = paymentTypes;
        }
    }
}