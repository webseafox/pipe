using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RemoveDevice
{
    public class RemoveDeviceOutput : IUseCaseOutput
    {
        public long DeviceId {  get; set; }

        public RemoveDeviceOutput() { }
        public RemoveDeviceOutput(long deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
