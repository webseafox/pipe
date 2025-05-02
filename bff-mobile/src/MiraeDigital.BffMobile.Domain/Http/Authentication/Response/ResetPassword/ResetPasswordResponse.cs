using System;

namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Response.ResetPassword
{
    public class ResetPasswordResponse
    {
        public Guid Token { get; set; }
        public long Document { get; set; }
        public long UserId { get; set; }
    }
}
