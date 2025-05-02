using MiraeDigital.BffMobile.Domain.Enums.CustomerTwoFactor;
using System;

namespace MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor
{
    public class DeviceItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DeviceKey { get; set; }
        public long CustomerId { get; set; }
        public DeviceStatus DeviceStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
