using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Bank.Api
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "5CF0636B1DF2DBD6790EF6D8D1553421E71BADBB",
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