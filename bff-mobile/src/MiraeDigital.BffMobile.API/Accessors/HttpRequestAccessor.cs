using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Primitives;
using MiraeDigital.BffMobile.Domain.SeedWork;

namespace MiraeDigital.BffMobile.API.Accessors
{
    public class HttpRequestAccessor : IRequestAccessor
    {

        public HttpRequestAccessor(IHttpContextAccessor accessor, IUserAccessor userAccessor)
        {
            Headers = accessor.HttpContext.Request.Headers.ToDictionary(s => s.Key, s => s.Value);
            OriginIP = ExtractClientIP(accessor.HttpContext.Request);
            User = userAccessor;
        }

        public string OriginIP { get; private set; }

        public Dictionary<string, StringValues> Headers { get; private set; }

        public IUserAccessor User { get; private set; }

        private string ExtractClientIP(HttpRequest request)
        {
            var headers = request.Headers;
            String ip = "";
            StringValues ips;

            if (string.IsNullOrWhiteSpace(ip) || System.Net.IPAddress.None.Equals(ip))
            {
                headers.TryGetValue(ForwardedHeaders.XForwardedFor.ToString(), out ips);
                ip = ips.FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(ip) || System.Net.IPAddress.None.Equals(ip))
            {
                headers.TryGetValue("ip", out ips);
                ip = ips.FirstOrDefault();
            }

            if ((string.IsNullOrWhiteSpace(ip) || System.Net.IPAddress.None.Equals(ip)) && request.HttpContext?.Connection?.RemoteIpAddress != null)
                ip = request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrWhiteSpace(ip) || System.Net.IPAddress.None.Equals(ip))
            {
                headers.TryGetValue("REMOTE_ADDR", out ips);
                ip = ips.FirstOrDefault();
            }


            return ip;
        }
    }
}
