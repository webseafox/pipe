using MiraeDigital.BffMobile.Domain.Http.Authentication.Request;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Request.Login;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Request.ResetPassword;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByPassword;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GetUser;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.Login;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.ResetPassword;
using Refit;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http.Authentication
{
    public interface IAuthenticationClient
    {
        [Post("/api/v2/Authenticate")]
        Task<LoginResponse> LoginAsync([Body] LoginRequest body);

        [Post("/api/v1/User/eletronicsignature/validate")]
        Task<ApiResponse<ValidateElectronicSignatureResponse>> ValidateElectronicSignatureAsync([Header("Authorization")] string token, [Body] ValidateElectronicSignatureRequest body);
        
        [Get("/api/v2/User/{document}")]
        Task<GetUserByDocumentResponse> GetUsersByDocumentAsync([AliasAs("document")] long document);

        [Multipart]
        [Post("/api/v1/Authenticate/token")]
        Task<ApiResponse<GenerateTokenByPasswordResponse>> GenerateTokenByPassword(
            [AliasAs("Document")] long document,
            [AliasAs("Password")] string password,
            [AliasAs("IpAddress")] string ipAddress);

        [Multipart]
        [Post("/api/v1/Authenticate/authorize")]
        Task<ApiResponse<GenerateTokenByAuthorizeResponse>> GenerateTokenByAuthorize(
            [Authorize("Bearer")] string jwtToken,
            [AliasAs("amrValue")] string amrValue, 
            [AliasAs("mfaToken")] string mfaToken);

        [Post("/api/v2/User/{userId}/password/reset")]
        Task<ResetPasswordResponse> ResetPasswordAsync([AliasAs("userId")] long userId, [Body] ResetPasswordRequest body);
    }
}
