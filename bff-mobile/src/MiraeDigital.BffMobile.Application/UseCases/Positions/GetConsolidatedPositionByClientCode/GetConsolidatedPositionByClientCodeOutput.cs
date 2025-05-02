using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePositionTotal;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.VariableIncome.Client.Http.Responses;

namespace MiraeDigital.BffMobile.Application.UseCases.Positions.GetConsolidatedPositionByClientCode
{
    public class GetConsolidatedPositionByClientCodeOutput : IUseCaseOutput
    {
        public GetConsolidatedPositionByClientCodeOutput()
        {
            
        }
        private GetConsolidatedPositionByClientCodeOutput(GetConsolidatedPositionGeneralByClientCodeResponse positionGeneralByClientCodeResponse, GetFixedIncomePositionTotalResponse fixedIncomePositionTotalResponse, decimal consolidatedTreasuryDirect)
        {
            FutureConsolidatedValue = positionGeneralByClientCodeResponse.BmfConsolidatedValue + positionGeneralByClientCodeResponse.GoldConsolidatedValue;
            BtcConsolidatedValue = positionGeneralByClientCodeResponse.BtcConsolidatedValue;
            OptionConsolidatedValue = positionGeneralByClientCodeResponse.OptionConsolidatedValue;
            ShareConsolidatedValue = positionGeneralByClientCodeResponse.ShareConsolidatedValue;
            TermConsolidatedValue = positionGeneralByClientCodeResponse.TermConsolidatedValue;
            InvestmentFundsConsolidatedValue = positionGeneralByClientCodeResponse.InvestmentFundsConsolidatedValue;
            FixedIncomeConsolidatedValue = fixedIncomePositionTotalResponse != null ? (decimal)fixedIncomePositionTotalResponse.Total.TotalGrossAmount : 0;
            FixedIncomeConsolidatedValue = FixedIncomeConsolidatedValue + consolidatedTreasuryDirect;
            Total = positionGeneralByClientCodeResponse.Total + FixedIncomeConsolidatedValue;
        }

        public decimal Total { get; set; }
        public decimal FutureConsolidatedValue { get; set; }
        public decimal BtcConsolidatedValue { get; set; }
        public decimal OptionConsolidatedValue { get; set; }
        public decimal ShareConsolidatedValue { get; set; }
        public decimal TermConsolidatedValue { get; set; }
        public decimal InvestmentFundsConsolidatedValue { get; set; }
        public decimal FixedIncomeConsolidatedValue { get; set; }

        public static GetConsolidatedPositionByClientCodeOutput ToOutput(GetConsolidatedPositionGeneralByClientCodeResponse positionGeneralByClientCodeResponse, GetFixedIncomePositionTotalResponse fixedIncomePositionTotalResponse, decimal consolidatedTreasuryDirect)
        {
            return new GetConsolidatedPositionByClientCodeOutput(positionGeneralByClientCodeResponse, fixedIncomePositionTotalResponse, consolidatedTreasuryDirect);
        }
    }
}
