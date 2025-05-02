namespace MiraeDigital.BffMobile.Domain.Http.FixedIncome.Requests.GetPagedOrder
{
    public class GetOrderPendingRequest
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public long InvestorId { get; set; }
    }
}
