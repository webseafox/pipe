using MiraeDigital.BffMobile.Application.Extensions;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.Lib.Application.UseCases;
using Newtonsoft.Json;
using Refit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.AuthorizationLevel
{
    public class AuthorizationLevelUseCase : IUseCase<AuthorizationLevelInput>
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IAuthenticationClient _authenticationClient;                
        private readonly ICryptoManager _cryptoManager;

        public AuthorizationLevelUseCase(IRequestAccessor requestAccessor, IAuthenticationClient authenticationClient, ICryptoManager cryptoManager)
        {
            _requestAccessor = requestAccessor;
            _authenticationClient = authenticationClient;
            _cryptoManager = cryptoManager;
        }

        public async Task<Output> Handle(AuthorizationLevelInput request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid(out Output outputValidation)) return outputValidation;

                request.Decrypt(_cryptoManager);

                var newAuthorizationLevel = await GetNewAuthorizationLevel(request);
                if (newAuthorizationLevel.output != null) return newAuthorizationLevel.output;

                GenerateTokenByAuthorizeResponse authorizationLevel = newAuthorizationLevel.authorization;

                AuthorizationLevelOutput output = new(_cryptoManager.Encrypt(authorizationLevel.AccesToken));
                return OutputBuilder.Create().WithResult(output).Response();
            }
            catch (Exception e)
            {
                return OutputBuilder.Create().WithError(e.Message).InternalError();
            }
        }
        
        private async Task<(GenerateTokenByAuthorizeResponse authorization, Output output)> GetNewAuthorizationLevel(AuthorizationLevelInput request)
        {
            Output output = null;

            if (string.IsNullOrEmpty(request.MfaToken))
            {
                output = OutputBuilder.Create().WithError("MFA Token is required.").UnauthorizedError();
                return(null, output);
            }

            ApiResponse<GenerateTokenByAuthorizeResponse> apiAuthorizeResponse = await _authenticationClient.GenerateTokenByAuthorize(_requestAccessor.User.AuthorizationToken.RemoveBearer(), request.MfaTokenMethod, request.MfaToken);

            if (!apiAuthorizeResponse.IsSuccessStatusCode)
            {
                OutputBuilder builder = OutputBuilder.Create();
                OutputError apiResponseError = JsonConvert.DeserializeObject<OutputError>(apiAuthorizeResponse.Error.Content);
                apiResponseError?.Errors?.ToList().ForEach((e) => builder.WithError(e.Message));

                output = builder.UnauthorizedError();
                return (null, output);
            }

            return (apiAuthorizeResponse.Content, output);
        }
    }
}
