﻿using System;
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
                "d070bd9d25d97ee4ca3f4bdfa8693af86e258fe0",
                "96a0a90af35aaf548a6438f58d490208a689f786",
                "be8fcdb64f9c3d8ce217991f06bdc61703d58615"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}