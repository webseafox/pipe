using AutoFixture;
using MiraeDigital.BffMobile.Application.UseCases.Account.GetBasicInformation;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GetUser;
using MiraeDigital.BffMobile.Domain.Http.Registration;
using MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer;
using MiraeDigital.BffMobile.Domain.Http.Suitability;
using MiraeDigital.BffMobile.Domain.Http.Suitability.Responses.GetSuitabilityCurrent;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using Moq;
using Refit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Account.GetBasicInformation
{
    public class AccountControllerTests : IClassFixture<WebApiFactory>
    {
        readonly Fixture fixture = new Fixture();
        readonly WebApiFactory _factory;        
        const string URL = "api/v1/Account/basic/information";
        const long USERID = 123456789;
        const int TEST_EXCEPTION_NULL_RETURN = 1;
        const int TEST_BAD_REQUEST_RETURN = 2;

        public AccountControllerTests(WebApiFactory factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Get Basic Information By Token returns success.")]
        public async Task GetBasicInformationByToken_Success()
        {
            HttpClient _client = _factory
                .ReplaceServiceTransient(GetRegistrationClientMock().Object)
                .ReplaceServiceTransient(GetSuitabilityClientMock().Object)
                .ReplaceServiceTransient(GetAuthenticationMock().Object)
                .CreateClient();

            GetBasicInformationOutput output = await _client.SendAsync<GetBasicInformationInput, GetBasicInformationOutput>(URL, token: GetToken());
            Assert.NotNull(output);

        }

        [Fact(DisplayName = "Get Basic Information By Token returns bad request.")]
        public async Task GetBasicInformationByToken_BadRequest()
        {
            HttpClient _client = _factory
                .ReplaceServiceTransient(GetRegistrationClientMock(TEST_BAD_REQUEST_RETURN).Object)
                .ReplaceServiceTransient(GetSuitabilityClientMock().Object)
                .ReplaceServiceTransient(GetAuthenticationMock().Object)
                .CreateClient();

            OutPutExtension output = await _client.SendAsync<GetBasicInformationInput, OutPutExtension>(URL, token: GetToken());
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)output.ErrorCode);
        }

        [Fact(DisplayName = "Get Basic Information By Token returns internal error.")]
        public async Task GetBasicInformationByToken_InternalError()
        {
            HttpClient _client = _factory
                .ReplaceServiceTransient(GetRegistrationClientMock(TEST_EXCEPTION_NULL_RETURN).Object)
                .ReplaceServiceTransient(GetSuitabilityClientMock().Object)
                .ReplaceServiceTransient(GetAuthenticationMock().Object)
                .CreateClient();

            OutPutExtension output = await _client.SendAsync<GetBasicInformationInput, OutPutExtension>(URL, token: GetToken());
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)output.ErrorCode);
        }

        private Mock<IRegistrationClient> GetRegistrationClientMock(int? testcode = null) 
        {            
            var mock = new Mock<IRegistrationClient>();
            mock.Setup(x => x.GetCustomerByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(() =>
                {
                    if (testcode == TEST_EXCEPTION_NULL_RETURN)
                        return null;

                    if (testcode == TEST_BAD_REQUEST_RETURN)
                        throw ApiException.Create(null, HttpMethod.Post, new HttpResponseMessage(HttpStatusCode.BadRequest), null).Result;

                    return fixture.Create<GetCustomerResponse>();
                });
         
            return mock;
        }

        private static string GetToken(string loaLevel = LoaLevels.Loa3) 
            => JwtTokenFaker.Create().SetIds(userId: USERID.ToString()).Fake(loaLevel, AmrMethods.App);

        private Mock<ISuitabilityClient> GetSuitabilityClientMock() 
        {
            var mock = new Mock<ISuitabilityClient>();
            mock.Setup(x => x.GetCurrentByDocumentAsync(It.IsAny<long>()))
                .ReturnsAsync(fixture.Create<SuitabilityCurrentResponse>);

            return mock;
        }
        private Mock<IAuthenticationClient> GetAuthenticationMock()
        {
            UserResponse userResponse = fixture.Create<UserResponse>();
            userResponse.UserId = USERID;            
            GetUserByDocumentResponse getUserByDocumentResponse = fixture.Create<GetUserByDocumentResponse>();
            getUserByDocumentResponse.Logins = new[] { userResponse };

            var mock = new Mock<IAuthenticationClient>();
            mock.Setup(x => x.GetUsersByDocumentAsync(It.IsAny<long>()))
                .ReturnsAsync(getUserByDocumentResponse);

            return mock;
        }

    }
}
