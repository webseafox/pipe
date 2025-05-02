using FluentValidation;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice
{
    public class RegisterDeviceValidator : AbstractValidator<RegisterDeviceInput>
    {
        public RegisterDeviceValidator()
        {
            RuleFor(r => r.DeviceKey).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();                                 
        }
    }
}
