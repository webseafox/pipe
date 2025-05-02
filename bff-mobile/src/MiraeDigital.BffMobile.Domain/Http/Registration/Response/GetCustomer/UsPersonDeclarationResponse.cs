namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class UsPersonDeclarationResponse
    {
        public bool IsUsCitizen { get; set; }
        public bool IsUsBorn { get; set; }
        public bool IsUsResident { get; set; }
        public bool HasUsMailingAddress { get; set; }
        public bool HasUsLegalRepresentative { get; set; }
        public bool HasUsPhoneNumber { get; set; }
    }
}
