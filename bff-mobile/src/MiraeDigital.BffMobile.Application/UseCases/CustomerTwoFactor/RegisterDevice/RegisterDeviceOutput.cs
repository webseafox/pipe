using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice
{
    public class RegisterDeviceOutput : IUseCaseOutput
    {
        public RegisterDeviceOutput(string secretKey)
        {
            SecretKey = secretKey;
        }
        public string SecretKey { get; set; }
    }
}
