using Microsoft.Extensions.DependencyInjection;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RegisterDevice;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Request;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.RegistrationDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using Moq;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.CustomerTwoFactor.RegisterDevice
{
    public class DeviceControllerTest : IClassFixture<WebApiFactory>
    {
        private readonly WebApiFactory _factory;
        private readonly ICryptoManager _cryptoManager;
        
        private const string URL = "api/v1/customer-two-factor/device/registration";
        private const string SECRETKEY = "122345-1-[DEVICEAPP]nhnqrRzluMYkFt60y9DMn5VuC2g=";

        enum TestCase { Success, InternalError, MfaError }

        public DeviceControllerTest(WebApiFactory factory)
        {
            _factory = factory;
            
            using var scope = _factory.Services.CreateScope();
            _cryptoManager = scope.ServiceProvider.GetService<ICryptoManager>();
        }

        [Fact]
        public async Task RegisterDevice_ValidatorBadRequest()
        {
            string token = JwtTokenFaker.Create().SetIds(customerId: "1").Fake(LoaLevels.Loa2, AmrMethods.Email);
            var input = new RegisterDeviceInput() { ESignature = _cryptoManager.Encrypt("pass") };
            HttpClient client = _factory.ReplaceServiceTransient(GetAuthenticationClientMock().Object).CreateClient();
            OutPutExtension output = await client.SendAsync<RegisterDeviceInput, OutPutExtension>(URL, input, HttpMethod.Post, token: token);

            Assert.Equal(ErrorCode.BadRequest, output.ErrorCode);
        }

        [Fact]
        public async Task RegisterDevice_UnauthorizedLowerLOA()
        {
            string token = JwtTokenFaker.Create().SetIds(customerId: "1").Fake(LoaLevels.Loa1, AmrMethods.Email);
            var input = new RegisterDeviceInput();
            HttpClient client = _factory.ReplaceServiceTransient(GetAuthenticationClientMock().Object).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var output = await client.PostAsync(URL, JsonContent.Create(input));            
            Assert.Equal(HttpStatusCode.Forbidden, output.StatusCode);
        }

        [Fact]
        public async Task RegisterDevice_Success()
        {
            string token = JwtTokenFaker.Create().SetIds(customerId: "1").Fake(LoaLevels.Loa2, AmrMethods.Email);
            RegisterDeviceInput input = GetRegisterDeviceInput();
            
            HttpClient client = _factory.ReplaceServiceTransient(GetCustomerTwoFactorMock(TestCase.Success).Object)
                .ReplaceServiceTransient(GetAuthenticationClientMock().Object)
                .CreateClient();

            RegisterDeviceOutput output
                = await client.SendAsync<RegisterDeviceInput, RegisterDeviceOutput>(URL, input, HttpMethod.Post, token: token);

            Assert.NotNull(output);
            Assert.NotEmpty(output.SecretKey);
            string secretKeyDescrypted = _cryptoManager.Decrypt(output.SecretKey);
            Assert.Equal(SECRETKEY, secretKeyDescrypted);
        }

        [Fact]
        public async Task RegisterDevice_InternalServer()
        {
            string token = JwtTokenFaker.Create().SetIds(customerId: "2").Fake(LoaLevels.Loa2, AmrMethods.Email);
            RegisterDeviceInput input = GetRegisterDeviceInput();

            HttpClient client = _factory.ReplaceServiceTransient(GetCustomerTwoFactorMock(TestCase.InternalError).Object)
                .ReplaceServiceTransient(GetAuthenticationClientMock().Object)
                .CreateClient();

            OutPutExtension output = await client.SendAsync<RegisterDeviceInput, OutPutExtension>(URL, input, HttpMethod.Post, token: token, HttpStatusCode.InternalServerError);

            Assert.NotNull(output);
            Assert.Single(output.Errors);            
        }

        [Fact]
        public async Task RegisterDevice_MfaOtherErrors()
        {
            string token = JwtTokenFaker.Create().SetIds(customerId: "3").Fake(LoaLevels.Loa2, AmrMethods.Email);
            RegisterDeviceInput input = GetRegisterDeviceInput();

            HttpClient client = _factory.ReplaceServiceTransient(GetCustomerTwoFactorMock(TestCase.MfaError).Object)
                .ReplaceServiceTransient(GetAuthenticationClientMock().Object)
                .CreateClient();

            OutPutExtension output = await client.SendAsync<RegisterDeviceInput, OutPutExtension>(URL, input, HttpMethod.Post, token: token);

            Assert.NotNull(output);
            Assert.Single(output.Errors);
            Assert.Contains("MFA", output.Errors[0].Message);
        }

        private Mock<ICustomerTwoFactor> GetCustomerTwoFactorMock(TestCase testCase)
        {
            Mock<ICustomerTwoFactor> customerTwoFactor = new();            
            Output outputMfaBadRequest = OutputBuilder.Create().WithError("Any error.").BadRequestError();            
            ApiException apiExeption = ApiResponseFaker.GetApiException(HttpMethod.Post,outputMfaBadRequest);

            customerTwoFactor
                .Setup(h => h.RegisterDevice(It.IsAny<RegistrationDeviceRequest>()))
                .ReturnsAsync((RegistrationDeviceRequest input) =>
                {
                    if (testCase == TestCase.MfaError) throw apiExeption;
                    if (testCase == TestCase.InternalError) throw new Exception("");
                    
                    return new RegistrationDeviceResponse() { SecretKey = SECRETKEY };
                });

            return customerTwoFactor;
        }

        private Mock<IAuthenticationClient> GetAuthenticationClientMock()
        {
            Mock<IAuthenticationClient> mock = new();
            ValidateElectronicSignatureResponse validateResponse = new() { Result = Domain.Enums.SignatureResult.Validated };

            mock.Setup(m => m.ValidateElectronicSignatureAsync(It.IsAny<string>(), It.IsAny<ValidateElectronicSignatureRequest>()))
                .ReturnsAsync(ApiResponseFaker.GetSuccessApiResponse<ValidateElectronicSignatureResponse>(validateResponse, HttpMethod.Post));

            return mock;
        }

        private RegisterDeviceInput GetRegisterDeviceInput()
        {
            return new RegisterDeviceInput()
            {                
                DeviceKey = "test",                
                Name = "meu device",
                ESignature = _cryptoManager.Encrypt("123456")
            };
        }
    }
}
