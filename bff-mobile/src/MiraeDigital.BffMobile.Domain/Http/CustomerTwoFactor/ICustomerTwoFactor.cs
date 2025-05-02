using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.DisableDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.RegistrationDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.SendToken;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.TokenValidation;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.DisableDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.GetActiveDeviceByCustomerId;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.GetDevicesByCustomerId;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.SendToken;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.TokenValidation;
using Refit;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor
{
    public interface ICustomerTwoFactor
    {
        [Post("/api/v1/device")]
        Task<RegistrationDeviceResponse> RegisterDevice([Body] RegistrationDeviceRequest input);

        [Delete("/api/v1/device")]
        Task<ApiResponse<DisableDeviceResponse>> DisableDevice([Body] DisableDeviceRequest input);

        [Get("/api/v1/Device/customer/{customerId}")]
        Task<GetDevicesByCustomerIdResponse> GetDevicesByCustomerId([AliasAs("customerId")] long customerId);

        [Get("/api/v1/Device/customer/{customerId}/active")]
        Task<ApiResponse<GetActiveDeviceByCustomerIdResponse>> GetActiveDeviceByCustomerId([AliasAs("customerId")] long customerId);

        [Post("/api/v1/Token/send")]
        Task<SendTokenResponse> SendToken([Body] SendTokenRequest request);

        [Post("/api/v1/Token/validation")]
        Task<ApiResponse<TokenValidationResponse>> ValidateToken(TokenValidationRequest request);
    }
}
