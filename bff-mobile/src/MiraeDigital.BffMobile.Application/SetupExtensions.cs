using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using MiraeDigital.BffMobile.Application.Pipelines;
using MiraeDigital.BffMobile.Application.Services;
using MiraeDigital.BffMobile.Application.Services.CryptoServices;
using MiraeDigital.BffMobile.Application.Services.ElectronicSignature;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.Login;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Services;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.BffMobile.Domain.Services.ElectronicSignature;
using MiraeDigital.Lib.Application.Behaviors;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationConfigurationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCryptoManager(configuration);

            services.AddMediatR(typeof(LoginUseCase));

            services.AddScoped<IElectronicSignatureService, ElectronicSignatureService>();
            services.AddScoped<IMfaService, MfaService>();

            services.AddPipelineBehavior();

            services.AddAuthorization(options =>
            {
                AddLoAPolicy(options, "RequireLoA1", LoaLevels.Loa1);
                AddLoAPolicy(options, "RequireLoA2", LoaLevels.Loa2);
                AddLoAPolicy(options, "RequireLoA3", LoaLevels.Loa3);
            });

            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.MapInboundClaims = false;
            });

            return services;
        }

        private static void AddCryptoManager(this IServiceCollection services, IConfiguration configuration)
        {
            CryptokeyConfig cryptoKeyConfig = configuration.GetSection("AuthenticationConfig").Get<CryptokeyConfig>();
            services.AddSingleton(cryptoKeyConfig);
            services.AddScoped<ICryptoManager, CryptoManager>();
        }

        private static void AddPipelineBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UseCaseExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ElectronicSignatureBehavior<,>));
        }

        private static void AddLoAPolicy(AuthorizationOptions options, string policyName, string requiredLoA)
        {
            options.AddPolicy(policyName, policy => policy.RequireAssertion(context =>
            {
                var userLoa = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Acr)?.Value;

                var loaOrder = new List<string> { LoaLevels.Loa1, LoaLevels.Loa2, LoaLevels.Loa3 };

                if (userLoa == null || !loaOrder.Contains(userLoa))
                {
                    return false;
                }

                return loaOrder.IndexOf(userLoa) >= loaOrder.IndexOf(requiredLoA);
            }));
        }
    }
}
