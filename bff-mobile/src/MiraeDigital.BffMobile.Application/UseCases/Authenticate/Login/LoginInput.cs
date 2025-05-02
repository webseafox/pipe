using FluentValidation;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.Login
{
    public class LoginInput : IUseCaseInput
    {
        bool decrypted = false;

        public string Document { get; set; }
        public string Password { get; set; }        
        public string DeviceKey { get; set; }
        public string MfaToken { get; set; }
        
        public bool IsValid(out Output outputValidation)
        {
            outputValidation = null;
            var result = new LoginValidator().Validate(this);

            if (!result.IsValid)
            {
                OutputBuilder builder = OutputBuilder.Create();
                foreach (var error in result.Errors)
                {
                    builder.WithError(error.ErrorMessage);
                }

                outputValidation = builder.BadRequestError();
            }

            return result.IsValid;
        }

        public void Decrypt(ICryptoManager cryptoManager)
        {
            if (decrypted) return;

            Document = cryptoManager.Decrypt(Document);
            Password = cryptoManager.Decrypt(Password);

            if(!string.IsNullOrEmpty(MfaToken))
                MfaToken = cryptoManager.Decrypt(MfaToken);

            decrypted = true;   
        }
    }

    class LoginValidator : AbstractValidator<LoginInput>
    {
        public LoginValidator()
        {
            RuleFor(r => r.Document).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();            
        }
    }

}
