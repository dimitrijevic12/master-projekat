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
                "c093e90e3e9a38e1cf2a5da6bbccfd7e2134140b",
                "d666d46e3f6f32efd3331c76082f6e0c8b6a3bac",
                "33ef6b94028bbb422e7894963051744100f90f46"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}