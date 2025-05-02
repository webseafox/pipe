using MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Services.ElectronicSignature;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice
{
    public class RegisterDeviceInput : IElectronicSignatureInput, IUseCaseInput
    {        
        public string DeviceKey { get; set; }
        public string Name { get; set; }        
        public string ESignature { get; set; }        
        public GeoLocationDto GeoLocation { get; set; }

        public bool Validate(out OutputBuilder builder)
        {
            builder = OutputBuilder.Create();
            RegisterDeviceValidator validator = new();

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
