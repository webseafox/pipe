using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MiraeDigital.BffMobile.IntegrationTests.Utils
{
    public class JsonContent<T> : StringContent
    {
        public JsonContent(T entity) : base(JsonConvert.SerializeObject(entity))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
