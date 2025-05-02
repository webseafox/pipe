using Microsoft.AspNetCore.Authentication.JwtBearer;
using MiraeDigital.Account.Client.Http;
using MiraeDigital.Account.Client.Http.Responses;
using MiraeDigital.BffMobile.Application.UseCases.CheckingAccount.GetCheckingAccountBalance;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.CheckingAccount.GetCheckingAccountBalance
{
    public class UserControllerTests : IClassFixture<WebApiFactory>
    {
        readonly WebApiFactory _factory;
        const string URL = "/api/v1/user/account/balance";

        const int TEST_EXCEPTION = 1;

        const decimal EXAMPLE_ACCOUNT_AVAILABLE_BALANCE = 1000;
        const decimal EXAMPLE_ACCOUNT_TOTAL_BALANCE = 2000;
        
        public UserControllerTests(WebApiFactory factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Should return account balance")]
        public async Task Should_ReturnAccountBalance()
        {            
            var accountClientMock = GetAccountClientMockAsync();
            var client = _factory
                
                .ReplaceServiceScoped(accountClientMock.Object)
                .CreateClient();

            var output = await client.SendAsync<GetCheckingAccountBalanceInput, GetCheckingAccountBalanceOutput>(URL, null, HttpMethod.Get, GetToken());

            Assert.Equal(EXAMPLE_ACCOUNT_AVAILABLE_BALANCE, output.AvailableBalance);
            Assert.Equal(EXAMPLE_ACCOUNT_TOTAL_BALANCE, output.TotalBalance);
        }
        
        [Fact(DisplayName = "Should internal server error when account query returns any error")]
        public async Task ShouldInternalServerErrorWhenAccountQueryReturnsAnyError()
        {            
            var accountClientMock = GetAccountClientMockAsync(TEST_EXCEPTION);
            var client = _factory
                
                .ReplaceServiceScoped(accountClientMock.Object)
                .CreateClient();

            var req = new HttpRequestMessage(HttpMethod.Get, URL);
            req.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, GetToken());
            var output = await client.SendAsync(req);

            Assert.Equal(HttpStatusCode.InternalServerError, output.StatusCode);
        }

        private static string GetToken(string loaLevel = LoaLevels.Loa3) => JwtTokenFaker.Create().Fake(loaLevel, AmrMethods.App);

        public static Mock<IAccountHttpClient> GetAccountClientMockAsync(int? testcode = null)
        {
            Mock<IAccountHttpClient> accountClientMock = new();

            accountClientMock.Setup(x => x.GetCheckingAccountByClientCodeAsync(It.IsAny<long>()))
                .ReturnsAsync((long investorId) =>
                {
                    if (testcode == TEST_EXCEPTION)
                        throw new Exception("Any exception");
                    
                    return new GetCheckingAccountByClientCodeResponse()
                    {
                        AvailableBalance = (double)EXAMPLE_ACCOUNT_AVAILABLE_BALANCE,
                        TotalBalance = (double)EXAMPLE_ACCOUNT_TOTAL_BALANCE
                    };
                });

            return accountClientMock;
        }

    }
}
