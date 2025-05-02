using MiraeDigital.Account.Client.Http;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.CheckingAccount.GetCheckingAccountBalance
{
    public sealed class GetCheckingAccountBalanceUseCase : IUseCase<GetCheckingAccountBalanceInput>
    {
        private readonly IRefitHttpHandler _refitHttpHandler;
        private readonly IAccountHttpClient _accountClient;        
        private readonly IRequestAccessor _requestAccessor;

        public GetCheckingAccountBalanceUseCase(IRefitHttpHandler refitHttpHandler, IAccountHttpClient accountClient, IRequestAccessor requestAccessor)
        {
            _refitHttpHandler = refitHttpHandler;
            _accountClient = accountClient;            
            _requestAccessor = requestAccessor;
        }

        public async Task<Output> Handle(GetCheckingAccountBalanceInput request, CancellationToken cancellationToken)
        {
            var builder = OutputBuilder.Create();

            try
            {
                var accountBalanceResponse = await _accountClient.GetCheckingAccountByClientCodeAsync(_requestAccessor.User.InvestorID);
                var output = new GetCheckingAccountBalanceOutput(accountBalanceResponse);
                return builder.WithResult(output).Response();
            }
            catch
            {
                return builder.WithError("Error querying account balance information.").InternalError();
            }            
        }
    }
}
