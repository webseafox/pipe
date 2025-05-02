using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.SendToken
{
    public class SendTokenOutput : IUseCaseOutput
    {
        public string Target { get; set; }
        public int DeliveryMethod { get; set; }

        public SendTokenOutput(string target, int deliveryMethod)
        {
            Target = target;
            DeliveryMethod = deliveryMethod;
        }
    }
}
