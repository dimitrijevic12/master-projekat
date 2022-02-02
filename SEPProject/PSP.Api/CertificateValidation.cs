using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PSP.Api
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "AA925BA367138F17DF5627454E2A9847230E5D2F",
                "d666d46e3f6f32efd3331c76082f6e0c8b6a3bac",
                "33ef6b94028bbb422e7894963051744100f90f46",
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
