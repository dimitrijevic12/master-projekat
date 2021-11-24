using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Model
{
    public class RegisteredWebStore
    {
        private Guid Id { get; set; }
        private int WebStoreId { get; set; }
        private string WebStoreName { get; set; }
        private string Password { get; set; }
        private MailAddress EmailAddress { get; set; }
        private Uri SuccessUrl { get; set; }
        private Uri FailedUrl { get; set; }
        private Uri ErrorUrl { get; set; }
        public virtual IEnumerable<PaymentType> PaymentTypes { get; set; }

        public RegisteredWebStore(Guid id, int webStoreId, string webStoreName, string password, MailAddress emailAddress,
            Uri successUrl, Uri failedUrl, Uri errorUrl, IEnumerable<PaymentType> paymentTypes)
        {
            Id = id;
            WebStoreId = webStoreId;
            WebStoreName = webStoreName;
            Password = password;
            EmailAddress = emailAddress;
            SuccessUrl = successUrl;
            FailedUrl = failedUrl;
            ErrorUrl = errorUrl;
            PaymentTypes = paymentTypes;
        }
    }
}
