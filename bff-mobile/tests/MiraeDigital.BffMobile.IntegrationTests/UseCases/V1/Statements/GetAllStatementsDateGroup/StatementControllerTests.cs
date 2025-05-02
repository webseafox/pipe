using MiraeDigital.Account.Client.Http;
using MiraeDigital.Account.Client.Http.Enum;
using MiraeDigital.Account.Client.Http.Requests;
using MiraeDigital.Account.Client.Http.Responses;
using MiraeDigital.BffMobile.Application.UseCases.Statements.GetAllStatementsDateGroup;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Statements.GetAllStatementsDateGroup
{
    public class StatementControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly Mock<IAccountHttpClient> _accountHttpClient;        
        readonly WebApiFactory _factory;
        private readonly HttpClient _client;
        private readonly string token;
        private const string URL = "api/v1/account/statement";

        private const string unknownError = "Unknown Error";

        public StatementControllerTests(WebApiFactory factory)
        {
            _accountHttpClient = new();            
            _factory = factory;
            _client = _factory
                .ReplaceServiceTransient(_accountHttpClient.Object)                
                .CreateClient();
            token = JwtTokenFaker.Create().Fake(LoaLevels.Loa3, AmrMethods.App);
        }

        [Fact]
        public async Task GetAllStatementsDateGroup_Sucess()
        {
            var request = GetAllStatementsDateGroupInput();

            string newURL = $"{URL}?RegisterFrom={request.RegisterFrom.ToString("yyyy-MM-dd")}" +
                $"&RegisterTo={request.RegisterTo.ToString("yyyy-MM-dd")}" +
                $"&StatementType={request.StatementType}" +
                $"&Description={request.Description}";
                        
            _accountHttpClient
                .Setup(a => a.GetAllStatementsDateGroupAsync(It.IsAny<long>(), It.IsAny<GetAllStatementsDateGroupRequest>()))
                .ReturnsAsync(() => {
                    return new GetAllStatementsDateGroupResponse()
                    {
                        Statements = new List<GetStatementsDateGroupResponse>() {
                                    new GetStatementsDateGroupResponse() {
                                        LiquidationDate = "2022-03-10",
                                        QuantityTransactions = 1,
                                        AvailableBalance = 3.2,
                                        Statements = new List<GetStatementsResponse>()
                                    } }
                    };
                });

            var output
                = await _client
                    .SendAsync<GetAllStatementsDateGroupInput, GetAllStatementsDateGroupOutput>
                    (newURL, token: token, expectStatusCode: HttpStatusCode.OK);

            Assert.NotNull(output);
            Assert.Single(output.Statements);
            var statement = output.Statements.First();
            Assert.Equal("10/03/2022", statement.LiquidationDate.ToString("dd/MM/yyyy"));
            Assert.Equal(1, statement.QuantityTransactions);
            Assert.Equal(3.2, statement.AvailableBalance);
            Assert.Empty(statement.Statements);
        }


        [Fact]
        public async Task GetAllStatementsDateGroup_InternalError()
        {
            var request = GetAllStatementsDateGroupInput();

            string newURL = $"{URL}?RegisterFrom={request.RegisterFrom.ToString("yyyy-MM-dd")}" +
                $"&RegisterTo={request.RegisterTo.ToString("yyyy-MM-dd")}" +
                $"&StatementType={request.StatementType}" +
                $"&Description={request.Description}";

            _accountHttpClient
                .Setup(a => a.GetAllStatementsDateGroupAsync(It.IsAny<long>(), It.IsAny<GetAllStatementsDateGroupRequest>()))
                .ReturnsAsync(() => throw new Exception(unknownError));

            var output
                = await _client
                    .SendAsync<GetAllStatementsDateGroupInput, OutPutExtension>
                    (newURL, token: token, expectStatusCode: HttpStatusCode.InternalServerError);

            Assert.NotNull(output);
            Assert.Single(output.Errors);
            Assert.Contains(unknownError, output.Errors[0].Message);
        }
        
        private static GetAllStatementsDateGroupInput GetAllStatementsDateGroupInput()
        {
            var request = new GetAllStatementsDateGroupInput();
            request.RegisterFrom = new DateTime(2021, 10, 4, 0, 0, 0, DateTimeKind.Utc);
            request.RegisterTo = new DateTime(2022, 10, 5, 0, 0, 0, DateTimeKind.Utc);
            request.StatementType = StatementType.Outflow;
            request.Description = "Description";
            return request;
        }
    }
}
