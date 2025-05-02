namespace MiraeDigital.BffMobile.Domain.Dtos.Authentication
{
    public static class CustomClaimTypes
    {
        public const string UserId = "uid";
        public const string Document = "dnum";
        public const string Name = "nm";
        public const string UserName = "unm";
        public const string CustomerId = "cid";
        public const string InvestorId = "iid";
    }

    public record CustomClaimsValue(
        string UserId = null,
        string Document = null,
        string Name = null,
        string UserName = null,
        string CustomerId = null,
        string InvestorId = null
    );
}
