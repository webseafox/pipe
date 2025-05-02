using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.ResetPassword
{
    public sealed class ResetPasswordInput : IUseCaseInput
    {
        public string IpAddress { get; private set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public long Document { get; set; }

        public void SetIpAddress(string ipAddress)
        {
            IpAddress = ipAddress;
        }
    }
}
