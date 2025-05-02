namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Request
{
    public sealed class ValidateElectronicSignatureRequest
    {
        public string ProvidedSignature { get; set; }
        public string IpAddress { get; set; }
    }
}
