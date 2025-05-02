using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.SeedWork
{
    public interface IRequestAccessor
    {
        string OriginIP { get; }

        Dictionary<string, StringValues> Headers { get; }

        IUserAccessor User { get; }
    }

}
