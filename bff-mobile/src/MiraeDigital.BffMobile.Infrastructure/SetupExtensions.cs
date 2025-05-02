using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiraeDigital.BffMobile.Domain.AggregatesModel.AuthenticateAggregate;
using MiraeDigital.BffMobile.Infrastructure;
using MiraeDigital.BffMobile.Infrastructure.Repositories;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureConfigurationExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(option =>
            {
                option.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"));
            });

            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();

            return services;
        }

        public static void UpgradeDatabase(this Context context)
        {
            if (context is { Database: { } })
            {
                context.Database.Migrate();
            }
        }
    }
}
