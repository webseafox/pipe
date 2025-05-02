using AutoFixture;
using MiraeDigital.BffMobile.Application.UseCases.Products.GetProducts;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Products.GetProducts;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Http.Products;
using MiraeDigital.BffMobile.Domain.Http.Products.Responses.GetProducts;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Products.GetProducts
{
    public class ProductControllerTests : IClassFixture<WebApiFactory>
    {
        readonly WebApiFactory _factory;
        private readonly HttpClient _client;
        private readonly Mock<IProductClient> _productsClient;
        private const string URL = "api/v1/product";

        private const string unknownError = "Unknown Error";

        public ProductControllerTests(WebApiFactory factory)
        {
            _productsClient = new();
            _factory = factory;
            _client = _factory
                .ReplaceServiceTransient(_productsClient.Object)
                .CreateClient();
        }

        [Fact]
        public async Task GetProductsUseCase_Success()
        {
            var input = new GetProductsInput();

            _productsClient
                .Setup(p => p.GetAllProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new GetProductsResponse()
                {
                    ProductItems = new List<ProductItem>() { new Fixture().Create<ProductItem>() },
                    TotalPages = 1,
                    Limit = input.Limit,
                    Page = input.Page,
                    TotalRows = 1,
                });

            var output
                = await _client
                    .SendAsync<GetProductsInput, GetProductsOutput>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Single(output.ProductItems);
        }

        [Fact]
        public async Task GetProductsUseCase_ApiException()
        {
            var input = new GetProductsInput();

            var apiException = ApiResponseFaker.GetApiException(HttpMethod.Get,
                OutputBuilder.Create().WithError(unknownError).BadRequestError());

            _productsClient
                .Setup(p => p.GetAllProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(apiException);

            var output
                = await _client
                    .SendAsync<GetProductsInput, OutPutExtension>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Null(output.Result);
            Assert.Equal(ErrorCode.ServiceUnavailable, output.ErrorCode);
            Assert.Contains(unknownError, output.Errors[0].Message);
        }

        [Fact]
        public async Task GetProductsUseCase_InternalServer()
        {
            var input = new GetProductsInput();

            _productsClient
                .Setup(p => p.GetAllProductsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception(unknownError));

            var output
                = await _client
                    .SendAsync<GetProductsInput, OutPutExtension>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Null(output.Result);
            Assert.Equal(ErrorCode.InternalError, output.ErrorCode);
            Assert.Contains(unknownError, output.Errors[0].Message);
        }

        private static string GetToken(string loaLevel = LoaLevels.Loa3) => JwtTokenFaker.Create().Fake(loaLevel, AmrMethods.App);
    }
}
