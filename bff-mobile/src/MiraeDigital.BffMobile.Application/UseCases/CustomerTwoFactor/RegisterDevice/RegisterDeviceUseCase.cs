using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.RegistrationDevice;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.Lib.Application.UseCases;
using Newtonsoft.Json;
using Refit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice
{
    public class RegisterDeviceUseCase : IUseCase<RegisterDeviceInput>
    {
        private readonly ICustomerTwoFactor _customerTwoFactor;
        private readonly IRequestAccessor _requestAccessor;
        private readonly ILogger<RegisterDeviceUseCase> _logger;
        private readonly ICryptoManager _cryptoManager;
        
        public RegisterDeviceUseCase(ICustomerTwoFactor customerTwoFactor, IRequestAccessor requestAccessor, ILogger<RegisterDeviceUseCase> logger, ICryptoManager cryptoManager)
        {
            _customerTwoFactor = customerTwoFactor;
            _requestAccessor = requestAccessor;
            _logger = logger;
            _cryptoManager = cryptoManager;            
        }

        public async Task<Output> Handle(RegisterDeviceInput request, CancellationToken cancellationToken)
        {
            if (!request.Validate(out var builder))
                return builder.BadRequestError();

            try
            {
                var req = new RegistrationDeviceRequest(
                        _requestAccessor.User.CustomerID,
                        request.DeviceKey,
                        request.Name,
                        _requestAccessor.OriginIP,
                        request.GeoLocation != null ? new(request.GeoLocation.Latitude, request.GeoLocation.Longitude) : null
                    ); 
                var res = await _customerTwoFactor.RegisterDevice(req);

                return builder.WithResult(new RegisterDeviceOutput(_cryptoManager.Encrypt(res.SecretKey))).Response();
            }
            catch (ApiException aex)
            {
                string error = $"Error in customerTwoFactor. Message: {aex.Content}";
                _logger.LogError(aex, error);
                                
                OutputError apiResponseError = JsonConvert.DeserializeObject<OutputError>(aex.Content);
                apiResponseError?.Errors?.ToList().ForEach((e) => builder.WithError($"MFA: {e.Message}", e.Code));
                return builder.CustomError((ErrorCode)aex.StatusCode);
            }
            catch (Exception ex)
            {
                string error = $"Internal error in RegisterDeviceUseCase. Message: {ex.Message}";
                _logger.LogError(ex, error);
                return builder .WithError(error) .InternalError();
            }

        }                
    }
}
