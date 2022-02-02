using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace PCC.Api
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "B007D1B58F081F5F92A1B74C7E48916941A82C02",
                "5CF0636B1DF2DBD6790EF6D8D1553421E71BADBB",
                "cead0afac19ac3c14dc31ef0b6d16f04cf809aff",
                "96f5bc58286f32ad1aa342eefc27344e63aadf10",
                "c093e90e3e9a38e1cf2a5da6bbccfd7e2134140b"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}