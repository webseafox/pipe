using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.AuthorizationLevel;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Authenticate.AuthorizationLevel
{
    public class AuthenticateControllerTests : IClassFixture<WebApiFactory>
    {
        readonly WebApiFactory _factory;
        readonly ICryptoManager _cryptoManager;

        const string URL = "/api/v1/Authenticate/authorization";

        enum TestCase { Success, Exception, LevelChangeOrMfaErrors }

        public AuthenticateControllerTests(WebApiFactory factory)
        {
            _factory = factory;
            using var scope = _factory.Services.CreateScope();
            _cryptoManager = scope.ServiceProvider.GetService<ICryptoManager>();
        }
                
        [Fact(DisplayName = "Authorization With MFA Returns Success")]
        public async Task AuthorizationWithMfa_ReturnsSuccess()
        {
            var input = GetAuthorizationLevelInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock().Object).CreateClient();

            AuthorizationLevelOutput output = await client.SendAsync<AuthorizationLevelInput, AuthorizationLevelOutput>(URL, input, HttpMethod.Post, GetToken());
            output.Token.Should().NotBeNullOrEmpty();                
        }

        [Fact(DisplayName = "Authorization With MFA And Higher LOA Returns Success")]
        public async Task AuthorizationWithMfaAndHigherLOA_ReturnsSuccess()
        {
            var input = GetAuthorizationLevelInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock().Object).CreateClient();

            AuthorizationLevelOutput output = await client.SendAsync<AuthorizationLevelInput, AuthorizationLevelOutput>(URL, input, HttpMethod.Post, GetToken(LoaLevels.Loa3));
            output.Token.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "Authorization With Empty MFA Returns Bad Request")]
        public async Task AuthorizationWithEmptyMfa_ReturnsBadRequest()
        {
            var input = new AuthorizationLevelInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock().Object).CreateClient();

            OutPutExtension output = await client.SendAsync<AuthorizationLevelInput, OutPutExtension>(URL, input, HttpMethod.Post, GetToken());
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)output.ErrorCode);
        }
                
        [Fact(DisplayName = "Authorization With MFA Returns Unauthorized")]
        public async Task AuthorizationWithMfa_UnauthorizedError()
        {
            var input = GetAuthorizationLevelInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock(TestCase.LevelChangeOrMfaErrors).Object).CreateClient();

            OutPutExtension output = await client.SendAsync<AuthorizationLevelInput, OutPutExtension>(URL, input, HttpMethod.Post, GetToken());
            Assert.Equal((int)HttpStatusCode.Unauthorized, (int)output.ErrorCode);
        }

        [Fact(DisplayName = "Authorization With MFA Returns Internal Server Error")]
        public async Task AuthorizationWithMfa_InternalServerError()
        {
            var input = GetAuthorizationLevelInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock(TestCase.Exception).Object).CreateClient();

            OutPutExtension output = await client.SendAsync<AuthorizationLevelInput, OutPutExtension>(URL, input, HttpMethod.Post, GetToken());
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)output.ErrorCode);
        }
        
        private static string GetToken(string loaLevel = LoaLevels.Loa1) => JwtTokenFaker.Create().Fake(loaLevel, AmrMethods.Email);

        private AuthorizationLevelInput GetAuthorizationLevelInput()
        {
            return new()
            {
                MfaTokenMethod = AmrMethods.Email,
                MfaToken = _cryptoManager.Encrypt("976321")
            };
        }

        private Mock<IAuthenticationClient> GetAuthenticationClientMock(TestCase testCase = TestCase.Success)
        {
            Mock<IAuthenticationClient> mock = new();            
            mock.Setup(x => x.GenerateTokenByAuthorize(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string jwt, string amr, string mfatoken) =>
                {
                    if (testCase == TestCase.Exception)
                    {
                        throw new Exception("Unexpected error.");
                    }

                    if (testCase == TestCase.LevelChangeOrMfaErrors)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<GenerateTokenByAuthorizeResponse>(
                            OutputBuilder.Create().WithError("Any error in authorization level change").NotFoundError(),
                            HttpMethod.Post
                        );
                    }

                    string loa = amr == AmrMethods.App ? LoaLevels.Loa3 : LoaLevels.Loa2;
                    GenerateTokenByAuthorizeResponse response = new()
                    {
                        AccesToken = JwtTokenFaker.Create().Fake(loa, AmrMethods.Password),
                        TokenType = "Bearer",
                        ExpiresIn = TimeSpan.FromMinutes(30).TotalMilliseconds,
                    };

                    return ApiResponseFaker.GetSuccessApiResponse<GenerateTokenByAuthorizeResponse>(response, HttpMethod.Post);
                });

            return mock;
        }
    }
}
