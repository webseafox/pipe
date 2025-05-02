using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Serilog;
using Serilog.Events;
using System;
using System.Net.Http;

namespace MiraeDigital.BffMobile.IntegrationTests.Extensions
{
    public class WebApiFactory : WebApplicationFactory<Program>
    {
        public WebApiFactory() { }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("JWT_SECURITY_KEY", "ejdbuUgKuzDCSLcolNNaDRHldWtp1p8S");

            builder.ConfigureServices(services =>
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                    .MinimumLevel.Override("System", LogEventLevel.Error)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(".log.test")
                    .CreateLogger();
            });
        }

        protected override void ConfigureClient(HttpClient client)
        {
            // Set headher with localhost ip address.
            client.DefaultRequestHeaders.Add("X-Forwarded-For", "127.0.0.1");            
        }
    }
}
