using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.WebApi.Authorization;

namespace MiraeDigital.BffMobile.API.Accessors
{
    public class HttpUserAccessor : IUserAccessor
    {
        private IHttpContextAccessor _accessor;
        public HttpUserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;

            if (_accessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
            {
                UserAuthenticated user = _accessor.HttpContext.GetUserFromJwtToken();

                UserID = user?.UserId ?? 0;
                UserName = user?.Username;
                Document = user?.Document ?? 0;
                CustomerID = user?.CustomerId ?? 0;
                InvestorID = user?.InvestorId ?? 0;
                Name = user?.Name;
                AuthorizationToken = _accessor.HttpContext?.Request.Headers.Authorization;
                IsAuthenticated = true;

            }            
            
        }

        public string Name { get; private set; }

        public string UserName { get; private set; }

        public long UserID { get; private set; }

        public long Document { get; private set; }

        public long CustomerID { get; private set; }

        public long InvestorID { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string AuthorizationToken { get; private set; }
    }
}
