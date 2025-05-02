using FluentValidation;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.AuthorizationLevel
{
    public class AuthorizationLevelInput : IUseCaseInput
    {
        bool decrypted = false;        
        public string MfaTokenMethod { get; set; }
        public string MfaToken { get; set; }
        
        public bool IsValid(out Output outputValidation)
        {            
            outputValidation = null;            
            var result = new AuthorizationLevelValidator().Validate(this);

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
                        
            MfaToken = cryptoManager.Decrypt(MfaToken);

            decrypted = true;   
        }
    }

    class AuthorizationLevelValidator : AbstractValidator<AuthorizationLevelInput>
    {
        public AuthorizationLevelValidator()
        {
            RuleFor(r => r.MfaTokenMethod).NotEmpty();
            RuleFor(r => r.MfaToken).NotEmpty();         
        }
    }
}
