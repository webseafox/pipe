namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class ProfessionalInformationResponse
    {
        public string ExternalProfessionCode { get; set; }
        public string ProfessionDescription { get; set; }
        public string ExtraProfessionDescription { get; set; }
        public bool HasPartnership { get; set; }
        public string CompanyName { get; set; }
        public long CompanyDocumentNumber { get; set; }
    }
}
