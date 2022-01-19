using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway
{
    public class CertificateDelegatingHandler : DelegatingHandler
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CertificateDelegatingHandler(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var inner = this.InnerHandler;
            while (inner is DelegatingHandler)
            {
                inner = ((DelegatingHandler)inner).InnerHandler;
            }
            // inner is HttpClientHandler
            if (inner is HttpClientHandler)
            {
                if (request.RequestUri.Equals("https://localhost:44300/api/transactions/status"))
                {
                    var httpClientHandler = (HttpClientHandler)inner;
                    var cert = new X509Certificate2($"{_webHostEnvironment.ContentRootPath}\\clientcertbank.pfx",
                    "Client123", X509KeyStorageFlags.PersistKeySet);
                    httpClientHandler.ClientCertificates.Add(cert);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}