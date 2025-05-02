using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.SendToken;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Enums.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.SendToken;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.SendToken;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.CustomerTwoFactor.SendToken
{
    public class CustomerTwoFactorControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly WebApiFactory _factory;        
        private readonly string token;
        private const string URL = "api/v1/customer-two-factor/token/send";

        enum TestCase { Success, InternalErrorForAnyException, InternalErrorForApiClientError }

        public CustomerTwoFactorControllerTests(WebApiFactory factory)
        {
            _factory = new WebApiFactory();
            token = JwtTokenFaker.Create().Fake(LoaLevels.Loa2, AmrMethods.Email);
        }

        [Fact]
        public async Task SendToken_ReturnsSuccess()
        {         
            SendTokenInput input = new SendTokenInput() { DeliveryMethod = (int)TokenMethodHotp.Email };
            
            HttpClient client = _factory.ReplaceServiceTransient(GetCustomerTwoFactorMock(TestCase.Success).Object).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage result = await client.PostAsync(URL, JsonContent.Create(input));
                        
            Assert.True(result.IsSuccessStatusCode);
            SendTokenOutput output = await result.Content.ReadFromJsonAsync<SendTokenOutput>();
            output.Target.Should().NotBeNullOrEmpty();
            output.DeliveryMethod.Should().Be((int)TokenMethodHotp.Email);
        }

        [Fact]
        public async Task SendToken_ReturnsInternalErrorForApiClientError()
        {
            SendTokenInput input = new SendTokenInput() { DeliveryMethod = (int)TokenMethodHotp.Email };

            HttpClient client = _factory.ReplaceServiceTransient(GetCustomerTwoFactorMock(TestCase.InternalErrorForApiClientError).Object).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage result = await client.PostAsync(URL, JsonContent.Create(input));

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            OutPutExtension output = await result.Content.ReadFromJsonAsync<OutPutExtension>();
            output.Errors.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task SendToken_ReturnsInternalErrorForAnyException()
        {
            SendTokenInput input = new SendTokenInput() { DeliveryMethod = (int)TokenMethodHotp.Email };

            HttpClient client = _factory.ReplaceServiceTransient(GetCustomerTwoFactorMock(TestCase.InternalErrorForAnyException).Object).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage result = await client.PostAsync(URL, JsonContent.Create(input));

            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
            OutPutExtension output = await result.Content.ReadFromJsonAsync<OutPutExtension>();
            output.Errors.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        private Mock<ICustomerTwoFactor> GetCustomerTwoFactorMock(TestCase testCase)
        {
            Mock<ICustomerTwoFactor> customerTwoFactor = new();                                    
            Output outputSendTokenError = OutputBuilder.Create().WithError("Any error.").BadRequestError();
            
            customerTwoFactor
                .Setup(h => h.SendToken(It.IsAny<SendTokenRequest>()))
                .ReturnsAsync((SendTokenRequest input) =>
                {
                    if (testCase == TestCase.InternalErrorForApiClientError) throw ApiResponseFaker.GetApiException(HttpMethod.Post, outputSendTokenError);
                    if (testCase == TestCase.InternalErrorForAnyException) throw new Exception("Unknown exception");

                    return new SendTokenResponse() { Target = $"customer@mail.com", DeliveryMethod = (int)TokenMethodHotp.Email };
                });

            return customerTwoFactor;
        }

    }
}
