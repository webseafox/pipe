using MiraeDigital.BffMobile.Domain.Enums.Registration;
using System;

namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class DocumentIdentityResponse
    {
        public string Number { get; set; }
        public IdentityDocumentType Type { get; set; }
        public string TypeDescription { get; set; }
        public DateTime IssueDate { get; set; }
        public IdentityDocumentIssuer Issuer { get; set; }
        public string IssuerDescription { get; set; }
        public string IssueState { get; set; }
        public long SecurityNumber { get; set; }
    }
}
