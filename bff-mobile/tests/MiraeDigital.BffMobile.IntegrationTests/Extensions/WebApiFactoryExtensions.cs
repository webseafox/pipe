using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MiraeDigital.BffMobile.IntegrationTests.Extensions
{
    public static class WebApiFactoryExtensions
    {
        public static WebApplicationFactory<Program> ReplaceServiceTransient<TFake>(this WebApplicationFactory<Program> factory, TFake fakeObj) where TFake : class
           => factory.WithWebHostBuilder(builder =>
           builder.ConfigureTestServices(service =>
           {
               service.Replace(ServiceDescriptor.Transient(x => fakeObj));
           }));

        public static WebApplicationFactory<Program> ReplaceServiceScoped<TFake>(this WebApplicationFactory<Program> factory, TFake fakeObj) where TFake : class
           => factory.WithWebHostBuilder(builder =>
           builder.ConfigureTestServices(service =>
           {
               service.Replace(ServiceDescriptor.Scoped(x => fakeObj));
           }));
    }
}
