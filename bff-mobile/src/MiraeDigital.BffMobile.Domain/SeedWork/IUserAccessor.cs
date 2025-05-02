namespace MiraeDigital.BffMobile.Domain.SeedWork
{
    public interface IUserAccessor
    {
        string Name { get; }
        string UserName { get; }
        long Document { get; }
        long UserID { get; }
        long CustomerID { get; }
        long InvestorID { get; }
        bool IsAuthenticated { get; }
        string AuthorizationToken { get; }
    }
}
