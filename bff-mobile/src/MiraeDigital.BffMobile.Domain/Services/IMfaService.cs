using MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Enums.Authentication;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Services
{
    public interface IMfaService
    {
        Task<long?> DisableDevice(long customerId, long deviceId, string ip);
        Task<DeviceItemDto> GetActiveDevice(long customerid);
        Task<DeviceIngressStatus> GetDeviceIngressStatus(long customerid, string deviceKey);
        Task<bool> TokenIsValid(string token, long customerId, int tokenMethod);
    }
}
