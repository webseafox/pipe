namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.DisableDevice
{
    public class DisableDeviceRequest
    {
        public long CustomerId { get; set; }
        public long DeviceId { get; set; }
        public string Ip { get; set; }

        public DisableDeviceRequest(){}
        public DisableDeviceRequest(long customerId, long deviceId, string ip)
        {
            CustomerId = customerId;
            DeviceId = deviceId;
            Ip = ip;
        }
    }
}
