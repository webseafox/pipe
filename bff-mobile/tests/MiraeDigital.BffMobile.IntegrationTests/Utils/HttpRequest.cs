using Microsoft.AspNetCore.Authentication.JwtBearer;
using MiraeDigital.Lib.Application.UseCases;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MiraeDigital.BffMobile.IntegrationTests.Utils
{
    public static class HttpRequest
    {
        public static async Task<O> SendAsync<T, O>(this HttpClient client, string url, T context = null, HttpMethod method = null, string token = null, HttpStatusCode? expectStatusCode = null) where T : class where O : class
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpRequestMessage request = new(method ?? HttpMethod.Get, url);
            
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    JwtBearerDefaults.AuthenticationScheme,
                    token);
            }

            if (context != null)
            {
                request.Content = new JsonContent<T>(context);
            }

            HttpResponseMessage res = await client.SendAsync(request);
            if(expectStatusCode != null)
            {
                Assert.Equal(expectStatusCode.Value, res.StatusCode);
            }
            string content = await res.Content.ReadAsStringAsync();
            O response = JsonConvert.DeserializeObject<O>(content);
            return response;
        }
    }

    public class OutPutExtension
    {
        public OutPutExtension(object result, IList<ErrorExtension> errors, ErrorCode? errorCode = null)
        {
            Result = result;
            Errors = errors;
            ErrorCode = errorCode;
        }

        public object Result { get; set; }
        public ErrorCode? ErrorCode { get; private set; }

        public IList<ErrorExtension> Errors { get; private set; }
    }

    public class ErrorExtension
    {
        public string Message { get; }

        public ErrorExtension(string message)
        {
            Message = message;
        }
    }
}
