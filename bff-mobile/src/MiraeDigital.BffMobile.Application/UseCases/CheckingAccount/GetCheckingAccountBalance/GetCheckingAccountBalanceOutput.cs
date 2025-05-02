using MiraeDigital.Account.Client.Http.Responses;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.CheckingAccount.GetCheckingAccountBalance
{
    public sealed class GetCheckingAccountBalanceOutput : IUseCaseOutput
    {
        public decimal TotalBalance { get; set; }
        public decimal AvailableBalance { get; set; }

        public GetCheckingAccountBalanceOutput() { }

        public GetCheckingAccountBalanceOutput(GetCheckingAccountByClientCodeResponse response)
        {
            TotalBalance = (decimal)response.TotalBalance;
            AvailableBalance = (decimal)response.AvailableBalance;
        }
    }
}
