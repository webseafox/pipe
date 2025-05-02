using System;

namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Response.Login
{
    public class LoginResponse
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long Document { get; set; }
        public string Token { get; set; }
        public bool HasEletronicSignature { get; set; }
        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public int DocumentType { get; set; }
        public DateTime LastAccess { get; set; }
        public string IpAddress { get; set; }
    }
}
