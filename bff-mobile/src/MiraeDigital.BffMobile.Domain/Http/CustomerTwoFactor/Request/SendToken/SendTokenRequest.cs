using MiraeDigital.BffMobile.Domain.Enums.CustomerTwoFactor;

namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.SendToken
{
    public class SendTokenRequest
    {
        public long CustomerId { get; set; }
        public int? DeliveryMethod { get; set; }
    }
}
