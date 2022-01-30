﻿using System;
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
                "a379a2ed534499ea50c197d79d802550d58484f1",
                "96a0a90af35aaf548a6438f58d490208a689f786"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}
