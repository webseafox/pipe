namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class AddressResponse
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool IsMailingAddress { get; set; }
    }
}
