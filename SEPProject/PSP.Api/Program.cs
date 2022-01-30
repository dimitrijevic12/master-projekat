using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Serilog;
using System;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace PSP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog();
                    webBuilder.ConfigureKestrel(options => {
                        var port = 44370;
                        var pfxFilePath = $"{AppContext.BaseDirectory}psp.pfx";
                        var pfxPassword = "12345";

                        options.Listen(IPAddress.Any, port, listenOptions => {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                            listenOptions.UseHttps(pfxFilePath, pfxPassword);
                        });
                    });
                });
    }
}
