using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiraeDigital.BffMobile.Application.UseCases.Products.GetProducts;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.Lib.Presenter.WebApi;
using MiraeDigital.Lib.WebApi.Controllers;
using MiraeDigital.Lib.WebApi.Conventions;

namespace MiraeDigital.BffMobile.API.UseCases.V1.Products.GetProducts
{
    [ApiVersion("1")]
    [ApiConventionType(typeof(ApiConventions))]
    public class ProductController : ApiControllerBase
    {
        private readonly IPresenter _presenter;
        public ProductController(IPresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Lista produtos.
        /// </summary>
        /// <response code="200">Retorno dos produtos</response>
        /// <response code="404">Indica que o não há produtos</response>
        /// <response code="500">Indica um erro interno do servidor</response>
        [Authorize(Policy = Policies.RequireLOA3)]
        [HttpGet]
        [ProducesResponseType(typeof(GetProductsOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProducts(int page, int limit)
        {
            var input = new GetProductsInput(page, limit);
            return await _presenter.Ok(input);
        }
    }
}
