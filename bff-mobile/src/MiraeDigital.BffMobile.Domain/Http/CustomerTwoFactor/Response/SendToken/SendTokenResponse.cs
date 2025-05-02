using MiraeDigital.BffMobile.Domain.Enums.CustomerTwoFactor;

namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.SendToken
{
    public class SendTokenResponse
    {
        public string Target { get; set; }
        public int DeliveryMethod { get; set; }
    }
}
