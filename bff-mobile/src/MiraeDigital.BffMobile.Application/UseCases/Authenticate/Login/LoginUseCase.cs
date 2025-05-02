using MiraeDigital.BffMobile.Application.Extensions;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Enums.Authentication;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByPassword;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Domain.Services;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.Lib.Application.UseCases;
using Newtonsoft.Json;
using Refit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.Login
{
    public class LoginUseCase : IUseCase<LoginInput>
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IAuthenticationClient _authenticationClient;        
        private readonly IMfaService _mfaService;
        private readonly ICryptoManager _cryptoManager;

        public LoginUseCase(
            IRequestAccessor requestAccessor,
            IAuthenticationClient authenticationClient,
            IMfaService mfaService,
            ICryptoManager cryptoManager)
        {
            _requestAccessor = requestAccessor;
            _authenticationClient = authenticationClient;            
            _mfaService = mfaService;
            _cryptoManager = cryptoManager;
        }

        public async Task<Output> Handle(LoginInput request, CancellationToken cancellationToken)
        {
            try
            {
                if(!request.IsValid(out Output outputValidation)) return outputValidation;

                request.Decrypt(_cryptoManager);

                var loginToken = await GetNewLoginToken(request);
                if (loginToken.output != null)  return loginToken.output;

                CustomClaimsValue jwtLoginData = loginToken.login.AccesToken.GetCustomClaimsValues();

                DeviceIngressStatus deviceIngressStatus = await _mfaService.GetDeviceIngressStatus(long.Parse(jwtLoginData.CustomerId), request.DeviceKey);

                GenerateTokenByAuthorizeResponse authorizationLevel = null;
                if(deviceIngressStatus == DeviceIngressStatus.Authorized)
                {
                    var newAuthorizationLevel = await GetNewAuthorizationLevel(request, loginToken.login.AccesToken);
                    if (newAuthorizationLevel.output != null) return newAuthorizationLevel.output;

                    authorizationLevel = newAuthorizationLevel.authorization;
                }

                LoginOutput output = new(_cryptoManager, loginToken.login, authorizationLevel, deviceIngressStatus);
                return OutputBuilder.Create().WithResult(output).Response();
            }
            catch (Exception e)
            {
                return OutputBuilder.Create().WithError(e.Message).InternalError();
            }
        }

        private async Task<(GenerateTokenByPasswordResponse login, Output output)> GetNewLoginToken(LoginInput request)
        {
            Output output = null;

            ApiResponse<GenerateTokenByPasswordResponse> apiLoginTokenResponse = await _authenticationClient.GenerateTokenByPassword(
                    long.Parse(request.Document),
                    request.Password,
                    _requestAccessor.OriginIP);

            if (!apiLoginTokenResponse.IsSuccessStatusCode)
            {
                OutputBuilder builder = OutputBuilder.Create();
                OutputError apiResponseError = JsonConvert.DeserializeObject<OutputError>(apiLoginTokenResponse.Error.Content);
                apiResponseError?.Errors?.ToList().ForEach((e) => builder.WithError(e.Message));

                output = builder.UnauthorizedError();
                return (null, output);
            }

            return (apiLoginTokenResponse.Content, output);
        }

        private async Task<(GenerateTokenByAuthorizeResponse authorization, Output output)> GetNewAuthorizationLevel(LoginInput request, string jwtToken)
        {
            Output output = null;

            if (string.IsNullOrEmpty(request.MfaToken))
            {
                output = OutputBuilder.Create().WithError("MFA Token is required.").UnauthorizedError();
                return(null, output);
            }

            ApiResponse<GenerateTokenByAuthorizeResponse> apiAuthorizeResponse = await _authenticationClient.GenerateTokenByAuthorize(jwtToken, AmrMethods.App, request.MfaToken);

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
