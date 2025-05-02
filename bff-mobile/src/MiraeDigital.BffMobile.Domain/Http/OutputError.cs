using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http
{
    public class OutputError
    {
        public OutputError()
        {
            
        }
        public int? ErrorCode { get; set; }
        public IList<Error> Errors { get; set; }
    }

    public sealed class Error
    {
        public string Message { get; set; }

        public string Code { get; set; }
    }
}
