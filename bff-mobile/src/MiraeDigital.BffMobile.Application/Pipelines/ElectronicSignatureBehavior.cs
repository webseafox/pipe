using MediatR;
using Microsoft.Extensions.Logging;
using MiraeDigital.BffMobile.Domain.Enums;
using MiraeDigital.BffMobile.Domain.Extensions;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.BffMobile.Domain.Services.ElectronicSignature;
using MiraeDigital.Lib.Application.UseCases;
using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.Pipelines
{
    internal sealed class ElectronicSignatureBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IElectronicSignatureInput
    {
        private readonly IElectronicSignatureService _electronicSignatureService;
        private readonly IRequestAccessor _requestAcessor;
        private readonly ILogger<ElectronicSignatureBehavior<TRequest, TResponse>> _logger;
        private readonly ICryptoManager _cryptoManager;

        public ElectronicSignatureBehavior(
            IElectronicSignatureService electronicSignatureService,
            IRequestAccessor requestAcessor,
            ICryptoManager cryptoManager,
            ILogger<ElectronicSignatureBehavior<TRequest, TResponse>> logger)
        {
            _electronicSignatureService = electronicSignatureService;
            _requestAcessor = requestAcessor;
            _cryptoManager = cryptoManager;
            _logger = logger;            
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var signatureResult = await _electronicSignatureService.Validate(
                    eSignature: _cryptoManager.Decrypt(request.ESignature),
                    ipAddress: _requestAcessor.OriginIP,
                    token: _requestAcessor.User.AuthorizationToken);

                if (signatureResult != SignatureResult.Validated)
                {
                    _logger.LogInformation("Invalid signature for {Command}: {SignatureResult}", typeof(TRequest).Name, signatureResult);
                    return (TResponse)Convert.ChangeType(OutputBuilder
                        .Create()
                        .WithError(signatureResult.GetDescription())
                        .BadRequestError(), typeof(TResponse));
                }
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "Error validating electronic signature for {Command}", typeof(TRequest).Name);
                return (TResponse)Convert.ChangeType(OutputBuilder
                    .Create()
                    .WithError(ex.Message)
                    .CustomError((ErrorCode)ex.StatusCode), typeof(TResponse));
            }

            return await next();
        }
    }
}
