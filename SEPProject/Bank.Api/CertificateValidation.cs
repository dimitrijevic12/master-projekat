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
                "96a0a90af35aaf548a6438f58d490208a689f786",
                "011b5975738583945f6574158dda3dcf97b13963",
                "d070bd9d25d97ee4ca3f4bdfa8693af86e258fe0",
                "a379a2ed534499ea50c197d79d802550d58484f1"
            };
            if (allowedThumbprints.Contains(clientCertificate.Thumbprint))
            {
                return true;
            }
            return false;
        }
    }
}