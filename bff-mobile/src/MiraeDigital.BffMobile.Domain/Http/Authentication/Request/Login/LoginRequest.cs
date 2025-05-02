
namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Request.Login
{
    public class LoginRequest
    {
        public long Document { get; set; }
        public string Passoword { get; set; }
        public string IpAddress { get; set; }
    }
}
