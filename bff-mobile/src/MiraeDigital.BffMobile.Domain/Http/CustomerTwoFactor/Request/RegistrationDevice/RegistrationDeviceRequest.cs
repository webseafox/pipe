using MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor;

namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.RegistrationDevice
{
    public class RegistrationDeviceRequest
    {
        public long CustomerId { get; set; }
        public string DeviceKey { get; set; }
        public string Name { get; set; }        
        public string Ip { get; set; }        
        public GeoLocationDto GeoLocation { get; set; }

        public RegistrationDeviceRequest() { }

        public RegistrationDeviceRequest(long customerId, string deviceKey, string name, string ip, GeoLocationDto geoLocation)
        {
            CustomerId = customerId;
            DeviceKey = deviceKey;
            Name = name;
            Ip = ip;
            GeoLocation = geoLocation;
        }
    }
}
