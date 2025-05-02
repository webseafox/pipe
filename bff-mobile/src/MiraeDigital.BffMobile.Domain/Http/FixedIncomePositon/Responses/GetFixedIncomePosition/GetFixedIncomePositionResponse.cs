using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePosition
{
    public class GetFixedIncomePositionResponse
    {
        public IEnumerable<GetFixedIncomePositionItemResponse> Positions { get; set; }
    }
}
