using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebShop.Api
{
    public class CertificateValidation
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            string[] allowedThumbprints = {
                "60296a06a8d6765b7947879e3b97430c3ad8e9a3",
                "AA925BA367138F17DF5627454E2A9847230E5D2F",
                "1008C317447BE55CA4C90237E0AF04DB20E03BF9",
                "d666d46e3f6f32efd3331c76082f6e0c8b6a3bac",
                "33ef6b94028bbb422e7894963051744100f90f46",
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
