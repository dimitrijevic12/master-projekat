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
                "1008C317447BE55CA4C90237E0AF04DB20E03BF9"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}
