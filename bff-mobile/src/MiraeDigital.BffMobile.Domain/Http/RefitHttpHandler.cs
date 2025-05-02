using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Refit;

namespace MiraeDigital.BffMobile.Domain.Http
{
    public class RefitHttpHandler : IRefitHttpHandler
    {
        public async Task<RefitResult<T>> Execute<T>(Func<Task<T>> func) where T : class, new()
        {
            List<string> errors = new List<string>();

            try
            {
                var result = await func();
                return new RefitResult<T>(result);
            }
            catch (ApiException ex)
            {
                ApiErrorOutput apiErrorOutput = await ex.GetContentAsAsync<ApiErrorOutput>();

                errors = apiErrorOutput?.Errors?.Select(x => x.Message).ToList() ?? new();

                return new RefitResult<T>(ex.Content, ex.StatusCode, errors);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return new RefitResult<T>(ex.Message, HttpStatusCode.InternalServerError, errors);
            }
        }
    }
}
