using System;
using MiraeDigital.BffMobile.Domain.Enums.Authentication;

namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GetUser
{
    public class UserResponse
    {
        public long UserId { get; set; }
        public long Document { get; set; }
        public DocumentType DocumentType { get; set; }
        public string Login { get; set; }
        public UserStatus UserStatus { get; set; }
        public DateTime LastAccess { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasEletronicSignature { get; set; }
    }
}
