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
                "CF91D8165460032DD2A7CE12170B14BC39316717"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}