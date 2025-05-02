namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GenerateTokenByPassword
{
    public sealed class GenerateTokenByPasswordResponse
    {
        public string AccesToken { get; set; }
        public string TokenType { get; set; }
        public double ExpiresIn { get; set; }
        public bool HasEletronicSignature { get; set; }
        public int Status { get; set; }
    }
}
