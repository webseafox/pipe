using MiraeDigital.BffMobile.Domain.Http.FixedIncome.Requests.GetPagedOrder;
using MiraeDigital.BffMobile.Domain.Http.FixedIncome.Responses.GetPagedOrder;
using MiraeDigital.BffMobile.Domain.Http.FixedIncome.Request.BrokerageNotes;
using Refit;
using System.IO;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.FixedIncome
{
    public interface IFixedIncomeClient
    {
        [Get("/api/v1/Order/pending")]
        Task<PageResponse<GetOrderPendingResponse>> GetPagedOrdersPending([Query] GetOrderPendingRequest filter);
        [Get("/api/v1/BrokerageNotes")]
        Task<ApiResponse<Stream>> DownlaodBrokerageNotes([Query] DownloadBrokerageNotesRequest filter);
    }
}
