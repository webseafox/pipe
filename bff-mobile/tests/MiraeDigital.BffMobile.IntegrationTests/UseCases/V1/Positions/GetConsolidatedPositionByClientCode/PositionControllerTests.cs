using Mirae.Commons.Model;
using MiraeDigital.BffMobile.Application.UseCases.Positions.GetConsolidatedPositionByClientCode;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon;
using MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePositionTotal;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using MiraeDigital.TreasuryDirect.Client.Http;
using MiraeDigital.TreasuryDirect.Client.Http.Requests;
using MiraeDigital.TreasuryDirect.Client.Http.Responses;
using MiraeDigital.VariableIncome.Client.Http;
using MiraeDigital.VariableIncome.Client.Http.Responses;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.Positions.GetConsolidatedPositionByClientCode
{
    public class PositionControllerTests : IClassFixture<WebApiFactory>
    {
        readonly WebApiFactory _factory;
        private readonly HttpClient _client;
        private readonly Mock<IVariableIncomeHttpClient> _variableIncomeHttpClient;        
        private readonly Mock<IFixedIncomePositionClient> _fixedIncomePositionClient;
        private readonly Mock<ITreasuryDirectHttpClient> _treasuryDirectHttpClient;        
        private const string URL = "api/v1/financial/position/invested";

        private const string unknownError = "Unknown Error";

        public PositionControllerTests(WebApiFactory factory)
        {
            _variableIncomeHttpClient = new();            
            _fixedIncomePositionClient = new();
            _treasuryDirectHttpClient = new();

            _factory = factory;
            _client = _factory
                .ReplaceServiceTransient(_variableIncomeHttpClient.Object)                
                .ReplaceServiceTransient(_fixedIncomePositionClient.Object)
                .ReplaceServiceTransient(_treasuryDirectHttpClient.Object)
                .CreateClient();            
        }

        [Fact]
        public async Task GetConsolidatedPositionByClientCodeInput_Sucess()
        {
            var input = new GetConsolidatedPositionByClientCodeInput();

            _variableIncomeHttpClient
                .Setup(v => v.GetConsolidatedPositionGeneralByClientCode(It.IsAny<long>()))
                .ReturnsAsync(new GetConsolidatedPositionGeneralByClientCodeResponse() { Total = 1 });
            _fixedIncomePositionClient
                .Setup(f => f.GetFixedIncomePositionTotalAsync(It.IsAny<long>()))
                .ReturnsAsync(new GetFixedIncomePositionTotalResponse() { Total = new GetFixedIncomePositionTotalItem()});
            _treasuryDirectHttpClient
                .Setup(t => t.GetInvestorMonthlyStatementAsync(It.IsAny<long>(), It.IsAny<GetInvestorMonthlyStatementRequest>()))
                .ReturnsAsync(new Page<GetInvestorMonthlyStatementResponse>(new List<GetInvestorMonthlyStatementResponse>(), 1, 1, 1));

            var output 
                = await _client
                    .SendAsync<GetConsolidatedPositionByClientCodeInput, GetConsolidatedPositionByClientCodeOutput>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Equal(1, output.Total);
        }

        [Fact]
        public async Task VariableIncomeHttpClient_ApiException()
        {
            var input = new GetConsolidatedPositionByClientCodeInput();

            var apiException = ApiResponseFaker.GetApiException(HttpMethod.Get,
                OutputBuilder.Create().WithError(unknownError).BadRequestError());

            _variableIncomeHttpClient
               .Setup(v => v.GetConsolidatedPositionGeneralByClientCode(It.IsAny<long>()))
               .ThrowsAsync(apiException);

            var output
                = await _client
                    .SendAsync<GetConsolidatedPositionByClientCodeInput, OutPutExtension>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Contains(unknownError, output.Errors[0].Message);
            Assert.Equal(ErrorCode.ServiceUnavailable, output.ErrorCode);
        }

        [Fact]
        public async Task FixedIncomePositionClient_ApiException()
        {
            var input = new GetConsolidatedPositionByClientCodeInput();
                
            var apiException = ApiResponseFaker.GetApiException(HttpMethod.Get,
                OutputBuilder.Create().WithError(unknownError).BadRequestError());

            _variableIncomeHttpClient
               .Setup(v => v.GetConsolidatedPositionGeneralByClientCode(It.IsAny<long>()))
               .ReturnsAsync(new GetConsolidatedPositionGeneralByClientCodeResponse() { Total = 1 });
            _fixedIncomePositionClient
                .Setup(f => f.GetFixedIncomePositionTotalAsync(It.IsAny<long>()))
                .ThrowsAsync(apiException);

            var output
                = await _client
                    .SendAsync<GetConsolidatedPositionByClientCodeInput, OutPutExtension>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Contains(unknownError, output.Errors[0].Message);
            Assert.Equal(ErrorCode.ServiceUnavailable, output.ErrorCode);
        }

        [Fact]
        public async Task TreasuryDirectHttpClient_ApiException()
        {
            var input = new GetConsolidatedPositionByClientCodeInput();

            var apiException = ApiResponseFaker.GetApiException(HttpMethod.Get,
                OutputBuilder.Create().WithError(unknownError).BadRequestError());

            _variableIncomeHttpClient
                .Setup(v => v.GetConsolidatedPositionGeneralByClientCode(It.IsAny<long>()))
                .ReturnsAsync(new GetConsolidatedPositionGeneralByClientCodeResponse() { Total = 1 });
            _fixedIncomePositionClient
                .Setup(f => f.GetFixedIncomePositionTotalAsync(It.IsAny<long>()))
                .ReturnsAsync(new GetFixedIncomePositionTotalResponse() { Total = new GetFixedIncomePositionTotalItem() });
            _treasuryDirectHttpClient
                .Setup(t => t.GetInvestorMonthlyStatementAsync(It.IsAny<long>(), It.IsAny<GetInvestorMonthlyStatementRequest>()))
                .ThrowsAsync(apiException);

            var output
                = await _client
                    .SendAsync<GetConsolidatedPositionByClientCodeInput, OutPutExtension>
                    (URL, input, HttpMethod.Get, GetToken());

            Assert.Contains(unknownError, output.Errors[0].Message);
            Assert.Equal(ErrorCode.ServiceUnavailable, output.ErrorCode);
        }

        private static string GetToken(string loaLevel = LoaLevels.Loa3) => JwtTokenFaker.Create().Fake(loaLevel, AmrMethods.App);

    }
}
