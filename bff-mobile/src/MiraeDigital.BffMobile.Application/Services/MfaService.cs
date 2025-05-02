using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Enums.Authentication;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.DisableDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.TokenValidation;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.DisableDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.GetActiveDeviceByCustomerId;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.TokenValidation;
using MiraeDigital.BffMobile.Domain.Services;
using Refit;
using System;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.Services
{
    public class MfaService : IMfaService
    {
        private readonly ILogger<MfaService> _logger;
        private readonly ICustomerTwoFactor _customerTwoFactor;

        public MfaService(ILogger<MfaService> logger, ICustomerTwoFactor customerTwoFactor)
        {
            _logger = logger;
            _customerTwoFactor = customerTwoFactor;
        }

        public async Task<DeviceIngressStatus> GetDeviceIngressStatus(long customerid, string deviceKey)
        {
            ApiResponse <GetActiveDeviceByCustomerIdResponse> apiActiveDeviceResponse = await _customerTwoFactor.GetActiveDeviceByCustomerId(customerid);

            if (!apiActiveDeviceResponse.IsSuccessStatusCode && apiActiveDeviceResponse.StatusCode != System.Net.HttpStatusCode.NotFound) 
            {
                const string errorMsg = "Device Status: Error getting active device to validate status.";
                _logger.LogError(apiActiveDeviceResponse.Error, errorMsg);
                throw new InvalidOperationException(errorMsg);
            }

            if (apiActiveDeviceResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return DeviceIngressStatus.FirstAccess;

            if (apiActiveDeviceResponse.Content.Device.DeviceKey == deviceKey)            
                return DeviceIngressStatus.Authorized;
                        
            return DeviceIngressStatus.ChangeDevice;
        }

        public async Task<DeviceItemDto> GetActiveDevice(long customerid)
        {
            ApiResponse<GetActiveDeviceByCustomerIdResponse> apiActiveDeviceResponse = await _customerTwoFactor.GetActiveDeviceByCustomerId(customerid);

            if (!apiActiveDeviceResponse.IsSuccessStatusCode && apiActiveDeviceResponse.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                const string errorMsg = "Device: Error getting active device.";
                _logger.LogError(apiActiveDeviceResponse.Error, errorMsg);
                throw new InvalidOperationException(errorMsg);
            }

            return apiActiveDeviceResponse.Content?.Device;
        }

        public async Task<long?> DisableDevice(long customerId, long deviceId, string ip)
        {
            DisableDeviceRequest request = new(customerId, deviceId, ip);
            ApiResponse<DisableDeviceResponse> apiDisableDeviceResponse = await _customerTwoFactor.DisableDevice(request);

            if (!apiDisableDeviceResponse.IsSuccessStatusCode && apiDisableDeviceResponse.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                const string errorMsg = "Device: Error deactivating device for account. Content: {Content}";
                _logger.LogError(apiDisableDeviceResponse.Error, errorMsg, apiDisableDeviceResponse.Error.Content);
                throw new InvalidOperationException("Device: Error deactivating device for account");
            }

            return apiDisableDeviceResponse.Content?.DeviceId;
        }

        public async Task<bool> TokenIsValid(string token, long customerId, int tokenMethod)
        {
            TokenValidationRequest validationRequest = new() { Token = token, CustomerId = customerId, TokenMethod = tokenMethod };
            ApiResponse<TokenValidationResponse> apiTokenValidationResponse = await _customerTwoFactor.ValidateToken(validationRequest);

            if (!apiTokenValidationResponse.IsSuccessStatusCode && apiTokenValidationResponse.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                const string errorMsg = "Error validating token.";
                _logger.LogError(apiTokenValidationResponse.Error, errorMsg);
                throw new InvalidOperationException(errorMsg);
            }

            return apiTokenValidationResponse.Content?.IsValid ?? false;
        }
    }
}
