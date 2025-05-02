using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Products.GetProducts
{
    public class GetProductsInput : IUseCaseInput
    {
        public GetProductsInput(int page = 1, int limit = 10)
        {
            Page = page;
            Limit = limit;
        }
        public int Page { get; }
        public int Limit { get; }
    }
}
