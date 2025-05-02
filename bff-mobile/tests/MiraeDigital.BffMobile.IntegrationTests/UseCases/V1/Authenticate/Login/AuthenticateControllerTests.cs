using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MiraeDigital.BffMobile.Application.UseCases.Authenticate.Login;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Enums.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByPassword;
using MiraeDigital.BffMobile.Domain.Services;
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

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Authenticate.Login
{
    public class AuthenticateControllerTests : IClassFixture<WebApiFactory>
    {       
        readonly WebApiFactory _factory;
        readonly ICryptoManager _cryptoManager;

        const string URL = "/api/v1/Authenticate";

        enum TestCase { Success, InternalError, Exception, LoginPasswordErrors, LevelChangeOrMfaErrors }


        public AuthenticateControllerTests(WebApiFactory factory)
        {
            _factory = factory;
            using var scope = _factory.Services.CreateScope();
            _cryptoManager = scope.ServiceProvider.GetService<ICryptoManager>();            
        }
        
        [Fact(DisplayName = "Login With Registered Device Returns Success.")]
        public async Task LoginWithRegisteredDevice_ReturnsSuccess()
        {
            LoginInput input = CreateLogintInput();
            input.MfaToken = _cryptoManager.Encrypt("123456");

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock().Object)                            
                            .ReplaceServiceScoped(GetDeviceServiceMock().Object)
                            .CreateClient();
            
            LoginOutput output = await client.SendAsync<LoginInput, LoginOutput>(URL, input, HttpMethod.Post);

            output.Name.Should().NotBeNullOrEmpty();
            output.InvestorId.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Login With Unregistered Device Returns Success.")]
        public async Task LoginWithUnregisteredDevice_ReturnsSuccess()
        {
            LoginInput input = CreateLogintInput();
            
            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock().Object)
                            .ReplaceServiceScoped(GetDeviceServiceMock(DeviceIngressStatus.FirstAccess).Object)
                            .CreateClient();

            LoginOutput output = await client.SendAsync<LoginInput, LoginOutput>(URL, input, HttpMethod.Post);

            output.Name.Should().NotBeNullOrEmpty();
            output.InvestorId.Should().BeGreaterThan(0);
        }

        [Fact(DisplayName = "Login With Unknown Error.")]
        public async Task LoginWithUnknownError_ReturnsInternalError()
        {
            LoginInput input = CreateLogintInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock(TestCase.Exception).Object)
                            .ReplaceServiceScoped(GetDeviceServiceMock().Object)
                            .CreateClient();

            OutPutExtension output = await client.SendAsync<LoginInput, OutPutExtension>(URL, input, HttpMethod.Post);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)output.ErrorCode);
        }

        [Fact(DisplayName = "Login With Registered Device Without Mfa Returns Unauthorized.")]
        public async Task LoginWithRegisteredDeviceWithoutMfa_ReturnsUnauthorizedError()
        {
            LoginInput input = CreateLogintInput();

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock().Object)                            
                            .ReplaceServiceScoped(GetDeviceServiceMock().Object)
                            .CreateClient();

            OutPutExtension output = await client.SendAsync<LoginInput, OutPutExtension>(URL, input, HttpMethod.Post);
            Assert.Equal((int)HttpStatusCode.Unauthorized, (int)output.ErrorCode);
        }


        [Fact(DisplayName = "Login Any Error In Password Validation Returns Unauthorized.")]
        public async Task LoginAnyErrorInPasswordValidation_ReturnsUnauthorizedError()
        {
            LoginInput input = CreateLogintInput();
            input.MfaToken = _cryptoManager.Encrypt("123456");

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock(TestCase.LoginPasswordErrors).Object)
                            .ReplaceServiceScoped(GetDeviceServiceMock().Object)
                            .CreateClient();

            OutPutExtension output = await client.SendAsync<LoginInput, OutPutExtension>(URL, input, HttpMethod.Post);
            Assert.Equal((int)HttpStatusCode.Unauthorized, (int)output.ErrorCode);
        }


        [Fact(DisplayName = "Login Any Error In Authorization Level Change Returns Unauthorized.")]
        public async Task LoginAnyErrorInAuthorizationLevelChange_ReturnsUnauthorizedError()
        {
            LoginInput input = CreateLogintInput();
            input.MfaToken = _cryptoManager.Encrypt("123456");

            var client = _factory.ReplaceServiceScoped(GetAuthenticationClientMock(TestCase.LevelChangeOrMfaErrors).Object)
                            .ReplaceServiceScoped(GetDeviceServiceMock().Object)
                            .CreateClient();

            OutPutExtension output = await client.SendAsync<LoginInput, OutPutExtension>(URL, input, HttpMethod.Post);
            Assert.Equal((int)HttpStatusCode.Unauthorized, (int)output.ErrorCode);
        }

        public LoginInput CreateLogintInput()
        {
            return new LoginInput()
            {
                Document = _cryptoManager.Encrypt("55387272890"), 
                Password = _cryptoManager.Encrypt(Faker.User.Password()),                
                DeviceKey = Guid.NewGuid().ToString(),
            };
        }

        public static Mock<IMfaService> GetDeviceServiceMock(DeviceIngressStatus deviceIngressStatus = DeviceIngressStatus.Authorized)
        {
            Mock<IMfaService> mock = new Mock<IMfaService>();
            mock.Setup(x => x.GetDeviceIngressStatus(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(deviceIngressStatus);

            return mock;
        }

        private Mock<IAuthenticationClient> GetAuthenticationClientMock(TestCase testCase = TestCase.Success) 
        {
            Mock<IAuthenticationClient> mock = new();

            mock.Setup(x => x.GenerateTokenByPassword(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((long doc, string pwd, string ip) =>
                {
                    if (testCase == TestCase.Exception)
                    {
                        throw new Exception("Unexpected error.");
                    }

                    if (testCase == TestCase.LoginPasswordErrors)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<GenerateTokenByPasswordResponse>(
                            OutputBuilder.Create().WithError("Login error!").NotFoundError(),
                            HttpMethod.Post
                        );
                    }

                    GenerateTokenByPasswordResponse responseLoginPassword = new()
                    {
                        AccesToken = JwtTokenFaker.Create().Fake(LoaLevels.Loa1, AmrMethods.Password),
                        TokenType = "Bearer",
                        ExpiresIn = TimeSpan.FromMinutes(30).TotalMilliseconds,
                        HasEletronicSignature = true,
                        Status = 1
                    };

                    return ApiResponseFaker.GetSuccessApiResponse<GenerateTokenByPasswordResponse>(responseLoginPassword, HttpMethod.Post);
                });


            mock.Setup(x => x.GenerateTokenByAuthorize(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((string jwt, string amr, string mfatoken) =>
                {
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
