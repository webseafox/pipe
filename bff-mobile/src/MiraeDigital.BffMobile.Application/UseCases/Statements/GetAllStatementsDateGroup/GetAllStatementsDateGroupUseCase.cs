using Microsoft.Extensions.Logging;
using MiraeDigital.Account.Client.Http;
using MiraeDigital.Account.Client.Http.Requests;
using MiraeDigital.BffMobile.Domain.Http.Registration;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Statements.GetAllStatementsDateGroup
{
    public class GetAllStatementsDateGroupUseCase : IUseCase<GetAllStatementsDateGroupInput>
    {
        private readonly IRequestAccessor _requestAccessor;
        private readonly IAccountHttpClient _accountHttpClient;
        private readonly ILogger<GetAllStatementsDateGroupUseCase> _logger;

        public GetAllStatementsDateGroupUseCase(IRequestAccessor requestAccessor, IAccountHttpClient accountHttpClient, IRegistrationClient registrationClient, 
            ILogger<GetAllStatementsDateGroupUseCase> logger)
        {
            _requestAccessor = requestAccessor;            
            _accountHttpClient = accountHttpClient;
            _logger = logger;
        }

        public async Task<Output> Handle(GetAllStatementsDateGroupInput request, CancellationToken cancellationToken)
        {
            try
            {
                var input = new GetAllStatementsDateGroupRequest
                {
                    RegisterFrom = request.RegisterFrom.ToString("dd/MM/yyyy"),
                    RegisterTo = request.RegisterTo.ToString("dd/MM/yyyy"),
                    StatementType = request.StatementType,
                    Description = request.Description
                };
                
                var statement = await _accountHttpClient.GetAllStatementsDateGroupAsync(_requestAccessor.User.InvestorID, input);

                var output = GetAllStatementsDateGroupOutput.ToOutput(statement);

                return OutputBuilder.Create().WithResult(output).Response();
            }
            catch (Exception ex)
            {
                string error = $"Internal error in bffMobile. Api GetAllStatementsDateGroupUseCase. Message: {ex.Message}";
                _logger.LogError(ex, error);
                return OutputBuilder.Create().WithError(error).InternalError();
            }
        }
    }
}
