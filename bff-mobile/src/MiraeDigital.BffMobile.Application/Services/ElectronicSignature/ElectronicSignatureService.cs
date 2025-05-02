using MiraeDigital.BffMobile.Domain.Enums;
using MiraeDigital.BffMobile.Domain.Http;
using MiraeDigital.BffMobile.Domain.Http.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Request;
using MiraeDigital.BffMobile.Domain.Services.ElectronicSignature;
using System.Net;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Application.Services.ElectronicSignature;

public class ElectronicSignatureService : IElectronicSignatureService
{
    private readonly IAuthenticationClient _authenticationClient;
    public ElectronicSignatureService(IAuthenticationClient authenticationClient, IRefitHttpHandler refitHttpHandler)
    {
        _authenticationClient = authenticationClient;        
    }

    public async Task<SignatureResult> Validate(string eSignature, string ipAddress, string token)
    {
        var validationRequest = new ValidateElectronicSignatureRequest
        {
            ProvidedSignature = eSignature,
            IpAddress = ipAddress
        };

        var validationResponse = await _authenticationClient.ValidateElectronicSignatureAsync(token, validationRequest);

        if (validationResponse.IsSuccessStatusCode) 
            return validationResponse.Content.Result;

        if (validationResponse.StatusCode == HttpStatusCode.BadRequest)
            return SignatureResult.Invalid;
        
        throw validationResponse.Error;        
    }
}
