using MiraeDigital.BffMobile.Domain.Enums.Registration;

namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class BankAccountResponse
    {
        public long BankAccountId { get; set; }
        public BankAccountType AccountType { get; set; }
        public int BankCode { get; set; }
        public int Agency { get; set; }
        public int Account { get; set; }
        public string Digit { get; set; }
        public string CoHolderName { get; set; }
        public long CoHolderDocument { get; set; }
        public bool IsDefault { get; set; }
    }
}
