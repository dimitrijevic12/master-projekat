using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CardPayment.Api
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "AA925BA367138F17DF5627454E2A9847230E5D2F",
                "5CF0636B1DF2DBD6790EF6D8D1553421E71BADBB",
                "d666d46e3f6f32efd3331c76082f6e0c8b6a3bac",
                "96f5bc58286f32ad1aa342eefc27344e63aadf10"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}