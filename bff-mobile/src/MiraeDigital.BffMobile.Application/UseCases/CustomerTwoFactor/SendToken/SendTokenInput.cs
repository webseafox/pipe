using FluentValidation;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.SendToken
{
    public class SendTokenInput : IUseCaseInput
    {
        public int? DeliveryMethod { get; set; }                
    }
}
