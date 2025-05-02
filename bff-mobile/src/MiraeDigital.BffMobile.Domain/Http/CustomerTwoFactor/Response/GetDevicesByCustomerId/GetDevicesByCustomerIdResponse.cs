using MiraeDigital.BffMobile.Domain.Enums.CustomerTwoFactor;
using System;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.GetDevicesByCustomerId
{
    public class GetDevicesByCustomerIdResponse
    {
        public List<DeviceItemResponse> Devices { get; set; }
    }

    public class DeviceItemResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DeviceKey { get; set; }
        public long CustomerId { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DeviceItemResponse() { }
    }

}
