using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.ValidateElectronicSignature;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Enums;
using MiraeDigital.BffMobile.Domain.Extensions;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Request;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using Moq;
using Refit;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Authenticate.ValidateElectronicSignature
{
    public class AuthenticateControllerTests : IClassFixture<WebApiFactory>
    {
        readonly WebApiFactory _factory;
        readonly ICryptoManager _cryptoManager;

        const string URL = "/api/v1/Authenticate/electronicsignature/validate";

        const int TEST_EXCEPTION = 1;
        const int TEST_USER_NOTFOUND = 2;
        const int TEST_INCORRECT_SIGNATURE = 3;

        public AuthenticateControllerTests(WebApiFactory factory)
        {
            _factory = factory;
            using var scope = _factory.Services.CreateScope();
            _cryptoManager = scope.ServiceProvider.GetService<ICryptoManager>();
        }

        [Fact(DisplayName = "Should validate signature when signature is correct")]
        public async Task Should_ValidateSignature_WhenSignatureCorrect()
        {
            ValidateElectronicSignatureInput input = GetValidateElectronicSignatureInput();

            var authenticationClientMock = await GetAuthenticationClientMockAsync();
            var client = _factory
                .ReplaceServiceScoped(authenticationClientMock.Object)
                .CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenFaker.Create().Fake(LoaLevels.Loa2, AmrMethods.Email));
            HttpResponseMessage result = await client.PostAsync(URL, JsonContent.Create(input));
            Assert.True(result.IsSuccessStatusCode);            
        }

        [Fact(DisplayName = "Should not validate signature when signature is incorrect")]
        public async Task Should_NotValidateSignature_WhenSignatureIncorrect()
        {
            ValidateElectronicSignatureInput input = GetValidateElectronicSignatureInput();

            var authenticationClientMock = await GetAuthenticationClientMockAsync(TEST_INCORRECT_SIGNATURE);
            var client = _factory
                .ReplaceServiceScoped(authenticationClientMock.Object)
                .CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtTokenFaker.Create().Fake(LoaLevels.Loa2, AmrMethods.Email));
            HttpResponseMessage result = await client.PostAsync(URL, JsonContent.Create(input));            
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact(DisplayName = "Should return Not found when user/signature is not found")]
        public async Task Should_ReturnNotFound_WhenUserOrSignatureNotFound()
        {
            ValidateElectronicSignatureInput input = GetValidateElectronicSignatureInput();

            var authenticationClientMock = await GetAuthenticationClientMockAsync(TEST_USER_NOTFOUND);
            var client = _factory
                .ReplaceServiceScoped(authenticationClientMock.Object)
                .CreateClient();

            var req = new HttpRequestMessage(HttpMethod.Post, URL)
            {
                Content = new JsonContent<ValidateElectronicSignatureInput>(input)
            };
            req.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, JwtTokenFaker.Create().Fake(LoaLevels.Loa2, AmrMethods.Email));
            var output = await client.SendAsync(req);

            Assert.Equal(HttpStatusCode.NotFound, output.StatusCode);
        }

        [Fact(DisplayName = "Should fail when internal server error")]
        public async Task Should_Fail_WhenInternalServerError()
        {
            ValidateElectronicSignatureInput input = GetValidateElectronicSignatureInput();

            var authenticationClientMock = await GetAuthenticationClientMockAsync(TEST_EXCEPTION);
            var client = _factory
                .ReplaceServiceScoped(authenticationClientMock.Object)
                .CreateClient();

            var req = new HttpRequestMessage(HttpMethod.Post, URL)
            {
                Content = new JsonContent<ValidateElectronicSignatureInput>(input)
            };
            req.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, JwtTokenFaker.Create().Fake(LoaLevels.Loa2, AmrMethods.Email));
            var output = await client.SendAsync(req);

            Assert.Equal(HttpStatusCode.InternalServerError, output.StatusCode);
        }

        private ValidateElectronicSignatureInput GetValidateElectronicSignatureInput() => new () { ESignature = _cryptoManager.Encrypt("my_signature") };

        public static async Task<Mock<IAuthenticationClient>> GetAuthenticationClientMockAsync(int? testcode = null)
        {
            Mock<IAuthenticationClient> authenticationClientMock = new();
                        
            ApiException apiInternalServerErrorException = await ApiException.Create(null, HttpMethod.Post, new HttpResponseMessage(HttpStatusCode.InternalServerError), null);

            authenticationClientMock.Setup(x => x.ValidateElectronicSignatureAsync(It.IsAny<string>(), It.IsAny<ValidateElectronicSignatureRequest>()))
                .ReturnsAsync((string token, ValidateElectronicSignatureRequest request) =>
                {
                    if (testcode == TEST_USER_NOTFOUND)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<ValidateElectronicSignatureResponse>(
                            OutputBuilder.Create().WithError("Not found!").NotFoundError(),
                            HttpMethod.Post);
                    }

                    if (testcode == TEST_EXCEPTION)
                        throw apiInternalServerErrorException;

                    if (testcode == TEST_INCORRECT_SIGNATURE)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<ValidateElectronicSignatureResponse>(
                            OutputBuilder.Create().WithError(SignatureResult.Invalid.GetDescription()).BadRequestError(),
                            HttpMethod.Post);                        
                    }

                    return ApiResponseFaker.GetSuccessApiResponse<ValidateElectronicSignatureResponse>(
                        new ValidateElectronicSignatureResponse() { Result = SignatureResult.Validated },
                        HttpMethod.Post);

                });

            return authenticationClientMock;
        }
    }
}
