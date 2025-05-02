using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.SendToken;
using MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Response.SendToken;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.Application.UseCases;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.UseCases.CustomerTwoFactor.SendToken
{
    public class SendTokenUseCase : IUseCase<SendTokenInput>
    {
        readonly ILogger<SendTokenUseCase> _logger;
        readonly IRequestAccessor _requestAccessor;
        readonly ICustomerTwoFactor _customerTwoFactor;

        public SendTokenUseCase(ILogger<SendTokenUseCase> logger, IRequestAccessor requestAccessor, ICustomerTwoFactor customerTwoFactor)
        {
            _logger = logger;
            _requestAccessor = requestAccessor;
            _customerTwoFactor = customerTwoFactor;
        }

        public async Task<Output> Handle(SendTokenInput request, CancellationToken cancellationToken)
        {
            try
            {
                SendTokenRequest sendTokenRequest = new() { CustomerId = _requestAccessor.User.CustomerID, DeliveryMethod = request.DeliveryMethod };
                SendTokenResponse sendTokenResponse = await _customerTwoFactor.SendToken(sendTokenRequest);

                return OutputBuilder.Create().WithResult(new SendTokenOutput(sendTokenResponse.Target, sendTokenResponse.DeliveryMethod)).Response();
            }
            catch (ApiException aex)
            {
                _logger.LogError(aex, "customer-two-factor api: Error sending authentication token: {0}", aex.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending authentication token");
            }

            return OutputBuilder.Create().WithError("Error sending authentication token").InternalError();
        }
    }
}
