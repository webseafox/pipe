using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePosition;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePositionRedemptionAvailable;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePositionTotal;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon
{
    public interface IFixedIncomePositionClient
    {
        [Get("/api/v1/FixedIncomePosition/total")]
        Task<GetFixedIncomePositionTotalResponse> GetFixedIncomePositionTotalAsync([Query] long investorId);

        [Get("/api/v2/FixedIncomePosition/synthetic")]
        Task<PageResponse<GetFixedIncomePositionItemResponse>> GetFixedIncomePositionAsync([Query] long investorId, [Query(CollectionFormat.Multi)] List<int> fixedIncomeType, [Query] int? liquidyType, [Query] int page, [Query] int limit);

        [Get("/api/v1/FixedIncomePosition/redemption/available")]
        Task<GetFixedIncomePositionRedemptionAvailableResponse> GetFixedIncomePositionRedemptionAvailableAsync([Query] long investorId);
    }
}
