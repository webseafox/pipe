using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.FixedIncome;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon;
using MiraeDigital.BffMobile.Domain.Http.Products;
using MiraeDigital.BffMobile.Domain.Http.Registration;
using MiraeDigital.BffMobile.Domain.Http.Suitability;
using Refit;

namespace MiraeDigital.BffMobile.API.Extensions
{
    public static class RefitExtensions
    {
        public static IServiceCollection AddRefitClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRefitHttpHandler, RefitHttpHandler>();

            services.AddRefitClient<ICustomerTwoFactor>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration.GetSection("CustomerTwoFactorClient:BaseUrl").Value);
                });
            
            services.AddScoped<IRefitHttpHandler, RefitHttpHandler>();

            services.AddRefitClient<IAuthenticationClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("AuthenticationClient:BaseUrl").Value);
            });

            services.AddRefitClient<IRegistrationClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("RegistrationClient:BaseUrl").Value);
            });

            services.AddRefitClient<IFixedIncomePositionClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("FixedIncomePositionClient:BaseUrl").Value);
            });

            services.AddRefitClient<IProductClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("ProductClient:BaseUrl").Value);
            });

            services.AddRefitClient<IFixedIncomeClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("FixedIncomeClient:BaseUrl").Value);
            });

            services.AddRefitClient<ISuitabilityClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("SuitabilityClient:BaseUrl").Value);
            });
            
            services.AddRefitClient<IFixedIncomeClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("FixedIncomeClient:BaseUrl").Value);
            });

            return services;
        }
    }
}
