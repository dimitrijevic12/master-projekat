using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace IssuerBank.Api
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "CF91D8165460032DD2A7CE12170B14BC39316717",
                "B007D1B58F081F5F92A1B74C7E48916941A82C02",
                "be8fcdb64f9c3d8ce217991f06bdc61703d58615",
                "d070bd9d25d97ee4ca3f4bdfa8693af86e258fe0",
                "011b5975738583945f6574158dda3dcf97b13963"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}