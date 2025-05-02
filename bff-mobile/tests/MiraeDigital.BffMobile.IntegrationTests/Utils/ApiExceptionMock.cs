using Refit;
using System.Net;
using System.Net.Http;

namespace MiraeDigital.BffMobile.IntegrationTests.Utils
{
    internal class ApiExceptionMock : ApiException
    {
        internal ApiExceptionMock(
            string httpMethod,
            string content,
            HttpStatusCode statusCode)
            : base(
                  new HttpRequestMessage(),
                  new HttpMethod(httpMethod),
                  content,
                  statusCode,
                  null,
                  default,
                  new RefitSettings())
        {
        }
    }
}
