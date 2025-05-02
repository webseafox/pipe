using MiraeDigital.BffMobile.Domain.Enums.Registration;
using System;

namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class QualifiedInvestorContractResponse
    {
        public long ContractId { get; set; }
        public DateTime AcceptedAt { get; set; }
        public ContractType Type { get; }
        public string Name { get; }
        public string IP { get; set; }
    }
}
