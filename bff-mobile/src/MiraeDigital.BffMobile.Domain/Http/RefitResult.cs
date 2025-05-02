using System.Collections.Generic;
using System.Net;

namespace MiraeDigital.BffMobile.Domain.Http
{
    public class RefitResult<T> where T : class
    {
        public RefitResult(T resultData)
        {
            ResultData = resultData;
        }

        public RefitResult(string errorContent, HttpStatusCode errorCode, List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
            ErroContent = errorContent;
            ErrorCode = errorCode;
        }

        public T ResultData { get; }
        public string ErroContent { get; }
        public List<string> ErrorMessages { get; } 
        public HttpStatusCode? ErrorCode { get; }
        public bool HasError() => ErrorCode != null;
    }

    public class ApiErrorOutput
    {
        public int? ErrorCode { get; private set; }

        public IList<ApiErrorItem> Errors { get; private set; }

        public ApiErrorOutput(IList<ApiErrorItem> errors, int? errorCode = null)
        {
            Errors = errors;
            ErrorCode = errorCode;
        }
    }

    public class ApiErrorItem
    {
        public string Message { get; private set; }

        public ApiErrorItem(string message)
        {
            Message = message;
        }
    }


}
