using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Request.ResetPassword;
using MiraeDigital.BffMobile.Domain.Http.Registration;
using MiraeDigital.Lib.Application.UseCases;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.ResetPassword
{
    public sealed class ResetPasswordUseCase : IUseCase<ResetPasswordInput>
    {
        private readonly IRefitHttpHandler _refitHttpHandler;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly IRegistrationClient _registrationClient;

        public ResetPasswordUseCase(IAuthenticationClient authenticationClient, IRegistrationClient registrationClient, IRefitHttpHandler refitHttpHandler)
        {
            _refitHttpHandler = refitHttpHandler;
            _authenticationClient = authenticationClient;
            _registrationClient = registrationClient;
        }

        public async Task<Output> Handle(ResetPasswordInput request, CancellationToken cancellationToken)
        {
            var builder = OutputBuilder.Create();

            var accountsResult = await _refitHttpHandler.Execute(async () => await _registrationClient.GetCustomerAccountsAsync(request.Document));

            if (accountsResult.ResultData != null && accountsResult.ResultData.Accounts.Any(x => x.Contacts.Email == request.Email))
            {
                var accounts = accountsResult.ResultData.Accounts.Where(x => x.Contacts.Email == request.Email);

                if (accounts != null)
                {
                    foreach (var item in accounts)
                    {
                        ResetPasswordRequest resetPasswordRequest = new ResetPasswordRequest()
                        {
                            IpAddress = request.IpAddress,
                            Url = request.Url
                        };

                        var resetPassword = await _refitHttpHandler.Execute(async () => await _authenticationClient.ResetPasswordAsync(item.UserId, resetPasswordRequest));
                    }
                }

                var output = ResetPasswordOutput.ToOutput(true, "Se houver conta vinculada para os dados informados, você receberá um email para redefinição de senha.");

                return builder.WithResult(output).Response();
            }
            else
            {
                builder.WithError("Se houver conta vinculada para os dados informados, você receberá um email para redefinição de senha.", ((int)ErrorCode.Business).ToString());
                return builder.CustomError(ErrorCode.Business);
            }
        }
    }
}
