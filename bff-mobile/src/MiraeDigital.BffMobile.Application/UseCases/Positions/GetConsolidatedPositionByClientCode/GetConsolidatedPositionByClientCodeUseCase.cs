using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.TreasuryDirect.Client.Http;
using MiraeDigital.TreasuryDirect.Client.Http.Requests;
using MiraeDigital.VariableIncome.Client.Http;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Positions.GetConsolidatedPositionByClientCode
{
    public class GetConsolidatedPositionByClientCodeUseCase : IUseCase<GetConsolidatedPositionByClientCodeInput>
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IVariableIncomeHttpClient _variableIncomeHttpClient;        
        private readonly IFixedIncomePositionClient _fixedIncomePositionClient;
        private readonly ITreasuryDirectHttpClient _treasuryDirectHttpClient;
        private readonly ILogger<GetConsolidatedPositionByClientCodeUseCase> _logger;
        public GetConsolidatedPositionByClientCodeUseCase(IRequestAccessor requestAccessor, IVariableIncomeHttpClient variableIncomeHttpClient,
            IRefitHttpHandler refitHttpHandler, IFixedIncomePositionClient fixedIncomePositionClient, ITreasuryDirectHttpClient treasuryDirectHttpClient, ILogger<GetConsolidatedPositionByClientCodeUseCase> logger)
        {
            _requestAccessor = requestAccessor;
            _variableIncomeHttpClient = variableIncomeHttpClient;            
            _fixedIncomePositionClient = fixedIncomePositionClient;
            _treasuryDirectHttpClient = treasuryDirectHttpClient;
            _logger = logger;
        }

        public async Task<Output> Handle(GetConsolidatedPositionByClientCodeInput request, CancellationToken cancellationToken)
        {
            try
            {
                var positionConsolidated
                    = await _variableIncomeHttpClient.GetConsolidatedPositionGeneralByClientCode(_requestAccessor.User.InvestorID);

                var fixedIncomePositionClient 
                    = await _fixedIncomePositionClient.GetFixedIncomePositionTotalAsync(_requestAccessor.User.InvestorID);

                var getInvestorMonthlyStatementRequest = new GetInvestorMonthlyStatementRequest
                {
                    PageSize = 10000,
                    Page = 1,
                };

                decimal consolidatedTreasuryDirect = 0;

                var response = await _treasuryDirectHttpClient.GetInvestorMonthlyStatementAsync(_requestAccessor.User.Document, getInvestorMonthlyStatementRequest);

                if (response != null && response.Items != null)
                {
                    foreach (var item in response.Items)
                    {
                        consolidatedTreasuryDirect += item.GrossAmount;
                    }
                }

                var output = GetConsolidatedPositionByClientCodeOutput.ToOutput(positionConsolidated, fixedIncomePositionClient, consolidatedTreasuryDirect);

                return OutputBuilder.Create().WithResult(output).Response();
            }
            catch (ApiException aex)
            {
                string error = $"Ex: API error -> Message: {aex.Content}";
                _logger.LogError(aex, error);
                return OutputBuilder.Create().WithError(error).CustomError(ErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                string error = $"Ex: Internal error in bffMobile. Api -> GetConsolidatedPositionByClientCodeUseCase. Message: {ex.Message}";
                _logger.LogError(ex, error);
                return OutputBuilder.Create().WithError(error).InternalError();
            }
        }
    }
}
