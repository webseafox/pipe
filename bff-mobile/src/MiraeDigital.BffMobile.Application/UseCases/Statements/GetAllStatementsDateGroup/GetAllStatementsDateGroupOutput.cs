using MiraeDigital.Account.Client.Http.Responses;
using MiraeDigital.BffMobile.Domain.Dtos.Statements;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Application.UseCases.Statements.GetAllStatementsDateGroup
{
    public sealed class GetAllStatementsDateGroupOutput : IUseCaseOutput
    {
        public GetAllStatementsDateGroupOutput()
        {
            
        }
        public GetAllStatementsDateGroupOutput(GetAllStatementsDateGroupDto entity)
        {
            Statements = entity.Statements;
        }

        public IEnumerable<StatementsDateGroupDto> Statements { get; set; }

        public static GetAllStatementsDateGroupOutput ToOutput(GetAllStatementsDateGroupResponse entity)
        {
            return new GetAllStatementsDateGroupOutput(ParseModel.Map<GetAllStatementsDateGroupDto>(entity));
        }
    }
}
