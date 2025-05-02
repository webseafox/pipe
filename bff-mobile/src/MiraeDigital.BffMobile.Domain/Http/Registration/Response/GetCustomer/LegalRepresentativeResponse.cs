using MiraeDigital.BffMobile.Domain.Enums.Registration;
using System;

namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class LegalRepresentativeResponse
    {
        public string Name { get; set; }
        public long Document { get; set; }
        public DocumentIdentityResponse IdentityDocument { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int MobileAreaCode { get; set; }
        public long MobileNumber { get; set; }
        public int PhoneAreaCode { get; set; }
        public long PhoneNumber { get; set; }
        public AddressResponse Address { get; set; }
        public Naturalization Naturalization { get; set; }
        public bool IsPoliticalyExposed { get; set; }
    }
}
