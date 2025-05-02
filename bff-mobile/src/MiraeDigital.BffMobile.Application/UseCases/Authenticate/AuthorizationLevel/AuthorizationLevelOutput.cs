using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.AuthorizationLevel
{
    public class AuthorizationLevelOutput : IUseCaseOutput
    {
        public string Token { get; set; }

        public AuthorizationLevelOutput() { }
        public AuthorizationLevelOutput(string token)
        {
            Token = token;
        }
    }
}
