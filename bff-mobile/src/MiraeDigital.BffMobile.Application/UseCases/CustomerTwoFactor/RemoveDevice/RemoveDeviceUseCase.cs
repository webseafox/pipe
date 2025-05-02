using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Domain.Services;
using MiraeDigital.Lib.Application.UseCases;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RemoveDevice
{
    public class RemoveDeviceUseCase : IUseCase<RemoveDeviceInput>
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly ILogger<RemoveDeviceUseCase> _logger;        
        private readonly IMfaService _mfaService;
        
        public RemoveDeviceUseCase(ILogger<RemoveDeviceUseCase> logger, IMfaService mfaService, IRequestAccessor requestAccessor)
        {            
            _logger = logger;            
            _mfaService = mfaService;            
            _requestAccessor = requestAccessor;
        }

        public async Task<Output> Handle(RemoveDeviceInput request, CancellationToken cancellationToken)
        {
            if (!request.Validate(out var builder))
                return builder.BusinessError();

            try
            {
                DeviceItemDto activeDevice = await _mfaService.GetActiveDevice(_requestAccessor.User.CustomerID);
                if (activeDevice is null) 
                    return OutputBuilder.Create().WithError("No active device for customer.").NotFoundError();

                if(!activeDevice.DeviceKey.Equals(request.DeviceKey, StringComparison.OrdinalIgnoreCase))
                    return OutputBuilder.Create().WithError("Removal must be performed on the device linked to the account.").BadRequestError();

                long? deviceIdDisabled = await _mfaService.DisableDevice(_requestAccessor.User.CustomerID, activeDevice.Id, _requestAccessor.OriginIP);
                if (deviceIdDisabled is null)
                    return OutputBuilder.Create().WithError("Device not found on removal.").NotFoundError();

                return OutputBuilder.Create().WithResult(new RemoveDeviceOutput(deviceIdDisabled.GetValueOrDefault())).Response();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error performing removal");
                return OutputBuilder.Create().WithError($"Error performing removal. {ex.Message}").InternalError();
            }
        }
    }
}
