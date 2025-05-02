using System;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Http
{
    public interface IRefitHttpHandler
    {
        Task<RefitResult<T>> Execute<T>(Func<Task<T>> func) where T : class, new();
    }
}
