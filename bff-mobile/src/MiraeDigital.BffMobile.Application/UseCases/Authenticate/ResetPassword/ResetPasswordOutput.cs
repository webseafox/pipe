using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.ResetPassword
{
    public sealed class ResetPasswordOutput : IUseCaseOutput
    {
        public ResetPasswordOutput(bool sucess, string message)
        {
            Sucess = sucess;
            Message = message;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }

        public static ResetPasswordOutput ToOutput(bool sucess, string message)
        {
            return new ResetPasswordOutput(sucess, message);
        }
    }
}
