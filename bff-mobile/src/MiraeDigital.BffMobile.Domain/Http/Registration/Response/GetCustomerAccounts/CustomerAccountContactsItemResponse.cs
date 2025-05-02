namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomerAccounts
{
    public class CustomerAccountContactsItemResponse
    {
        public string Email { get; set; }
        public string ComercialEmail { get; set; }
        public int? MobileAreaCode { get; set; }
        public long? MobileNumber { get; set; }
        public int? ComercialAreaCode { get; set; }
        public long? ComercialNumber { get; set; }
        public int? ResidencialAreaCode { get; set; }
        public long? ResidencialNumber { get; set; }
    }
}
