using FluentValidation;
namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RemoveDevice
{
    public class RemoveDeviceValidator : AbstractValidator<RemoveDeviceInput>
    {
        public RemoveDeviceValidator()
        {            
            RuleFor(r => r.DeviceKey).NotEmpty();                                    
        }
    }
}
