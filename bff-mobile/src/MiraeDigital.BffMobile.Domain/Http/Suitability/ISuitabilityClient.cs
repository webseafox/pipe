using MiraeDigital.BffMobile.Domain.Http.Suitability.Responses.GetSuitabilityCurrent;
using Refit;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.Suitability
{
    public interface ISuitabilityClient
    {
        [Get("/api/v1/Suitability/current/{document}")]
        [Headers("Authorization: Bearer")]
        Task<SuitabilityCurrentResponse> GetCurrentByDocumentAsync([Header("document")] long document);
    }
}
