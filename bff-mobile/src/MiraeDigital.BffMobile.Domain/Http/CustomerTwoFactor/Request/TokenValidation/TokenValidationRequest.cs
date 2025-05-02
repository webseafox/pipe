namespace MiraeDigital.BffMobile.Domain.Http.CustomerTwoFactor.Request.TokenValidation
{
    public class TokenValidationRequest
    {
        public long CustomerId { get; set; }
        public string Token { get; set; }
        public int TokenMethod { get; set; }
    }
}
