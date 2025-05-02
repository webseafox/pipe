using MiraeDigital.BffMobile.Application.Extensions;
using MiraeDigital.BffMobile.Domain.Dtos.Authentication;
using MiraeDigital.BffMobile.Domain.Enums.Authentication;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByPassword;
using MiraeDigital.BffMobile.Domain.Services.CryptoServices;
using MiraeDigital.Lib.Application.UseCases;

namespace MiraeDigital.BffMobile.Application.UseCases.Authenticate.Login
{
    public class LoginOutput : IUseCaseOutput
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Document { get; set; }
        public string Token { get; set; }
        public bool HasEletronicSignature { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public long InvestorId { get; set; }
        public long CustomerId { get; set; }
        public DeviceIngressStatus DeviceIngressStatus { get; set; }

        public LoginOutput() { }
        public LoginOutput(
            ICryptoManager cryptoManager,
            GenerateTokenByPasswordResponse loginToken, 
            GenerateTokenByAuthorizeResponse authorizationLevel, 
            DeviceIngressStatus deviceIngressStatus)
        {
            CustomClaimsValue loginResponse = loginToken.AccesToken.GetCustomClaimsValues();

            Token = cryptoManager.Encrypt(authorizationLevel?.AccesToken ?? loginToken.AccesToken);
            UserName = !string.IsNullOrEmpty(loginResponse.UserName) ? cryptoManager.Encrypt(loginResponse.UserName) : null;
            Document = !string.IsNullOrEmpty(loginResponse.Document) ? cryptoManager.Encrypt(loginResponse.Document) : null;
            Name = !string.IsNullOrEmpty(loginResponse.Document) ? cryptoManager.Encrypt(loginResponse.Name) : null;

            InvestorId = long.TryParse(loginResponse.InvestorId, out long valueInvestorId) ? valueInvestorId : 0;
            CustomerId = long.TryParse(loginResponse.CustomerId, out long valueCustomerId) ? valueCustomerId : 0;
            UserId = long.TryParse(loginResponse.UserId, out long valueUserId) ? valueUserId : 0;
            HasEletronicSignature = loginToken.HasEletronicSignature;
            Status = loginToken.Status;

            DeviceIngressStatus = deviceIngressStatus;
        }
                
    }
}
