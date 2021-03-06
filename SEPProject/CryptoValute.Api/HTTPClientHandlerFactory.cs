using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CryptoValute.Api
{
    public class HTTPClientHandlerFactory
    {
        public static HttpClientHandler Create(string path)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.Tls12;
            var cert = new X509Certificate2(path,
                "12345", X509KeyStorageFlags.PersistKeySet);
            handler.ClientCertificates.Add(cert);
            return handler;
        }
    }
}
