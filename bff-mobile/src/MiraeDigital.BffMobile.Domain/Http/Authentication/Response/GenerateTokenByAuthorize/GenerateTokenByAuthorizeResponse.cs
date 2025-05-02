namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByAuthorize
{
    public sealed class GenerateTokenByAuthorizeResponse
    {
        public string AccesToken { get; set; }
        public string TokenType { get; set; }
        public double ExpiresIn { get; set; }
    }
}
