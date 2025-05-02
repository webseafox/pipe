using MiraeDigital.BffMobile.Domain.Dtos.Products.GetProducts;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http.Products.Responses.GetProducts
{
    public class GetProductsResponse
    {
        public int TotalRows { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<ProductItem> ProductItems { get; set; }
    }
}
