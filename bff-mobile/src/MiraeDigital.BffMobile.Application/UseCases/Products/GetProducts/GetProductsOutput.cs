using MiraeDigital.BffMobile.Domain.Dtos.Products.GetProducts;
using MiraeDigital.BffMobile.Domain.Http.Products.Responses.GetProducts;
using MiraeDigital.Lib.Application.UseCases;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Application.UseCases.Products.GetProducts
{
    public class GetProductsOutput : IUseCaseOutput
    {
        public GetProductsOutput(int totalRows, int page, int limit, int totalPages, IEnumerable<ProductItem> productItems)
        {
            TotalRows = totalRows;
            Page = page;
            Limit = limit;
            TotalPages = totalPages;
            ProductItems = productItems;
        }

        public int TotalRows { get; }
        public int Page { get; }
        public int Limit { get; }
        public int TotalPages { get; }
        public IEnumerable<ProductItem> ProductItems { get; set; }

        public static GetProductsOutput ToOutput(GetProductsResponse response)
        {
            return new GetProductsOutput(response.TotalRows, response.Page, response.Limit, response.TotalPages, response.ProductItems);
        }
    }
}
