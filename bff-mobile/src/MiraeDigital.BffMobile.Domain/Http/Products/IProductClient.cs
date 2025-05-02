using MiraeDigital.BffMobile.Domain.Http.Products.Responses.GetProducts;
using Refit;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.Products
{
    public interface IProductClient
    {
        [Get("/api/v1/Product")]
        Task<GetProductsResponse> GetAllProductsAsync(int page, int limit);
    }
}
