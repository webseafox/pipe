using MiraeDigital.Lib.Application.UseCases;
using Refit;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace MiraeDigital.BffMobile.IntegrationTests.Fakers
{
    public static class ApiResponseFaker
    {
        public static ApiException GetApiException(HttpMethod method, Output output)
        {            
            return ApiException
                .Create(new HttpRequestMessage(method, ""),
                    method, 
                    new HttpResponseMessage((HttpStatusCode)output.ErrorCode) { Content = JsonContent.Create(output) }, 
                    null).Result;
        }

        public static ApiResponse<T> GetSuccessApiResponse<T>(T content, HttpMethod method)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(content) };

            var apiResponse = new ApiResponse<T>(response, content, new());
            return apiResponse;
        }

        public static ApiResponse<T> GetErrorApiResponse<T>(Output output, HttpMethod method)
        {
            HttpStatusCode statusCode = (HttpStatusCode)output.ErrorCode;

            var request = new HttpRequestMessage(method, "");

            var response = new HttpResponseMessage(statusCode) { Content = JsonContent.Create(output) };

            var apiException = ApiException
                .Create(request, method, response, null).Result;

            var apiResponse = new ApiResponse<T>(response, default(T), new RefitSettings(), apiException);
            return apiResponse;
        }
    }
}
