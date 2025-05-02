using MiraeDigital.Account.Client.Http;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.IPO.Client.Http;
using MiraeDigital.TreasuryDirect.Client.Http;
using MiraeDigital.VariableIncome.Client.Http;

namespace MiraeDigital.BffMobile.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClientExtention(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRefitHttpHandler, RefitHttpHandler>();

            services.AddHttpClient<IAccountHttpClient, AccountHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["Account:BaseUrl"]);
            });

            services.AddHttpClient<IVariableIncomeHttpClient, VariableIncomeHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["VariableIncome:BaseUrl"]);
            });

            services.AddHttpClient<IIPOHttpClient, IPOHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["IPO:BaseUrl"]);
            });

            services.AddHttpClient<ITreasuryDirectHttpClient, TreasuryDirectHttpClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["TreasuryDirect:BaseUrl"]);

            });

            return services;
        }
    }
}
