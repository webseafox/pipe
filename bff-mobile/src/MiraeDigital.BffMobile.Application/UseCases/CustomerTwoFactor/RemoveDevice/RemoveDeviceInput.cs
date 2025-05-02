using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RemoveDevice
{
    public class RemoveDeviceInput : IUseCaseInput
    {
        public string DeviceKey { get; set; }                   
  
        public bool Validate(out OutputBuilder builder)
        {
            builder = OutputBuilder.Create();
            RemoveDeviceValidator validator = new();

            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    builder.WithError(error.ErrorMessage);
                }
            }

            return result.IsValid;
        }
    }
}
