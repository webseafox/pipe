using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Http.Products;
using MiraeDigital.Lib.Application.UseCases;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.Products.GetProducts
{
    public class GetProductsUseCase : IUseCase<GetProductsInput>
    {
        private IProductClient _productsClient;
        private ILogger<GetProductsUseCase> _logger;
        public GetProductsUseCase(IProductClient productsClient, ILogger<GetProductsUseCase> logger)
        {
            _productsClient = productsClient;
            _logger = logger;
        }
        public async Task<Output> Handle(GetProductsInput request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productsClient.GetAllProductsAsync(request.Page, request.Limit);
                var output = GetProductsOutput.ToOutput(products);
                return OutputBuilder.Create().WithResult(output).Response();
            }
            catch (ApiException aex)
            {
                string message = $"Ex: Api error in Product. Message: {aex.Content}";
                _logger.LogError(aex, message);
                return OutputBuilder.Create().WithError(message).ServiceUnavailableError();
            }
            catch (Exception ex)
            {
                string message = $"Ex: Internal error in BffWebPortal. Api -> GetProductsUseCase. Message: {ex.Message}";
                _logger.LogError(ex, message);
                return OutputBuilder.Create().WithError(message).InternalError();
            }
        }
    }
}
