using System.Collections.Generic;
using System.Linq;

namespace MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GetUser
{
    public class GetUserByDocumentResponse
    {
        public IEnumerable<UserResponse> Logins { get; set; }

        public UserResponse GetUser(long userId) => Logins.FirstOrDefault(x => x.UserId == userId); 
    }
}
