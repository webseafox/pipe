using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http.FixedIncome
{
    public class PageResponse<T>
    {
        public int TotalRows { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Products { get; set; }
        public IEnumerable<T> Orders { get; set; }
    }
}
