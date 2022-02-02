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
                "CF91D8165460032DD2A7CE12170B14BC39316717",
                "96f5bc58286f32ad1aa342eefc27344e63aadf10",
                "cead0afac19ac3c14dc31ef0b6d16f04cf809aff",
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