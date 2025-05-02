using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Dtos.Statements
{
    public class GetAllStatementsDateGroupDto
    {
        public IEnumerable<StatementsDateGroupDto> Statements { get; set; }
    }
}
