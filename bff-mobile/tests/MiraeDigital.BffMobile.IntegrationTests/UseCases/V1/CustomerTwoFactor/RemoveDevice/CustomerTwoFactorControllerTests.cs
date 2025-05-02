using AutoFixture;
using MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.RemoveDevice;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Dtos.Token;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.DisableDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.DisableDevice;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.GetActiveDeviceByCustomerId;
using MiraeDigital.BffMobile.IntegrationTests.Extensions;
using MiraeDigital.BffMobile.IntegrationTests.Fakers;
using MiraeDigital.BffMobile.IntegrationTests.Utils;
using MiraeDigital.Lib.Application.UseCases;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.UseCases.V1.CustomerTwoFactor.RemoveDevice
{
    public class CustomerTwoFactorControllerTests : IClassFixture<WebApiFactory>
    {
        private readonly WebApiFactory _factory;
        
        private const string URL = "/api/v1/customer-two-factor/device/remove";

        readonly DeviceItemDto activeDevice = new Fixture().Create<DeviceItemDto>();

        enum TestCase { Success, NoActiveDevice, NotFoundWhenRemoving, ActiveDeviceUnknownError, ErrorWhenRemoving }

        public CustomerTwoFactorControllerTests(WebApiFactory factory)
        {
            _factory = new WebApiFactory();            
        }

        [Fact]
        public async Task RemoveDevice_FromActiveDevice_ReturnsSuccess()
        {
            var input = GetRemoveDeviceInput();
            var client = _factory.ReplaceServiceScoped(GetCustomerTwoFactorMock().Object).CreateClient();

            RemoveDeviceOutput output = await client.SendAsync<RemoveDeviceInput, RemoveDeviceOutput>(URL, input, HttpMethod.Delete, GetToken());
            Assert.Equal(activeDevice.Id, output.DeviceId);
        }

        [Fact]
        public async Task RemoveDevice_NoActiveDevice_ReturnsNotFound()
        {
            var input = GetRemoveDeviceInput();
            var client = _factory.ReplaceServiceScoped(GetCustomerTwoFactorMock(TestCase.NoActiveDevice).Object).CreateClient();

            OutPutExtension output = await client.SendAsync<RemoveDeviceInput, OutPutExtension>(URL, input, HttpMethod.Delete, GetToken());
            Assert.Equal((int)HttpStatusCode.NotFound, (int)output.ErrorCode);
        }

        [Fact]
        public async Task RemoveDevice_DeviceIsNotCurrent_ReturnsBadRequest()
        {
            var input = new RemoveDeviceInput() { DeviceKey = "not_curremt_active" };
            var client = _factory.ReplaceServiceScoped(GetCustomerTwoFactorMock().Object).CreateClient();

            OutPutExtension output = await client.SendAsync<RemoveDeviceInput, OutPutExtension>(URL, input, HttpMethod.Delete, GetToken());
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)output.ErrorCode);
        }

        [Fact]
        public async Task RemoveDevice_NotFoundWhenRemoving_ReturnsNotFound()
        {
            var input = GetRemoveDeviceInput();
            var client = _factory.ReplaceServiceScoped(GetCustomerTwoFactorMock(TestCase.NotFoundWhenRemoving).Object).CreateClient();

            OutPutExtension output = await client.SendAsync<RemoveDeviceInput, OutPutExtension>(URL, input, HttpMethod.Delete, GetToken());
            Assert.Equal((int)HttpStatusCode.NotFound, (int)output.ErrorCode);
        }

        [Fact]
        public async Task RemoveDevice_ActiveDeviceUnknownError_ReturnsInternalError()
        {
            var input = GetRemoveDeviceInput();
            var client = _factory.ReplaceServiceScoped(GetCustomerTwoFactorMock(TestCase.ActiveDeviceUnknownError).Object).CreateClient();

            OutPutExtension output = await client.SendAsync<RemoveDeviceInput, OutPutExtension>(URL, input, HttpMethod.Delete, GetToken());
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)output.ErrorCode);
        }

        [Fact]
        public async Task RemoveDevice_ErrorWhenRemoving_ReturnsInternalError()
        {
            var input = GetRemoveDeviceInput();
            var client = _factory.ReplaceServiceScoped(GetCustomerTwoFactorMock(TestCase.ErrorWhenRemoving).Object).CreateClient();

            OutPutExtension output = await client.SendAsync<RemoveDeviceInput, OutPutExtension>(URL, input, HttpMethod.Delete, GetToken());
            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)output.ErrorCode);
        }

        private string GetToken(string loaLevel = LoaLevels.Loa3) 
            => JwtTokenFaker.Create().SetIds(customerId: activeDevice.CustomerId.ToString()).Fake(loaLevel, AmrMethods.App);

        private RemoveDeviceInput GetRemoveDeviceInput(string deviceKey = null) => new() { DeviceKey = deviceKey ?? activeDevice.DeviceKey };

        private Mock<ICustomerTwoFactor> GetCustomerTwoFactorMock(TestCase testCase = TestCase.Success)
        {
            Mock<ICustomerTwoFactor> customerTwoFactor = new();
            
            customerTwoFactor
                .Setup(h => h.GetActiveDeviceByCustomerId(It.IsAny<long>()))
                .ReturnsAsync((long input) =>
                {
                    if (testCase == TestCase.NoActiveDevice)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<GetActiveDeviceByCustomerIdResponse>(
                            OutputBuilder.Create().WithError("Not Found!").NotFoundError(), 
                            HttpMethod.Delete);
                    }

                    if (testCase == TestCase.ActiveDeviceUnknownError)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<GetActiveDeviceByCustomerIdResponse>(
                            OutputBuilder.Create().WithError("Unknown error").InternalError(),
                            HttpMethod.Delete);
                    }
                    
                    GetActiveDeviceByCustomerIdResponse response = new() { Device = activeDevice };
                    return ApiResponseFaker.GetSuccessApiResponse(response, HttpMethod.Get);                    
                });

            customerTwoFactor
               .Setup(h => h.DisableDevice(It.IsAny<DisableDeviceRequest>()))
                .ReturnsAsync((DisableDeviceRequest input) =>
                {
                    if (testCase == TestCase.NotFoundWhenRemoving)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<DisableDeviceResponse>(
                            OutputBuilder.Create().WithError("Not Found!").NotFoundError(),
                            HttpMethod.Delete);
                    }

                    if (testCase == TestCase.ErrorWhenRemoving)
                    {
                        return ApiResponseFaker.GetErrorApiResponse<DisableDeviceResponse>(
                            OutputBuilder.Create().WithError("Unknown error").InternalError(),
                            HttpMethod.Delete);
                    }

                    DisableDeviceResponse response = new() { DeviceId = activeDevice.Id };
                    return ApiResponseFaker.GetSuccessApiResponse(response, HttpMethod.Delete);
                });

            return customerTwoFactor;
        }

    }
}
