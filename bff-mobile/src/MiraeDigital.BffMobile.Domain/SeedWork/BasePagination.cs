namespace MiraeDigital.BffMobile.Domain.SeedWork
{
    public class BasePagination
    {
        public BasePagination(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
        public int Limit { get; }

        public int Page { get; }
    }
}
