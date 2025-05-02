using System;

namespace MiraeDigital.BffMobile.Application.UseCases.Account.GetBasicInformation
{
    public class QualifiedInvestorContractItem
    {
        public long ContractId { get; set; }
        public DateTime? AcceptedAt { get; set; }

        public QualifiedInvestorContractItem() { }
        public QualifiedInvestorContractItem(long contractId, DateTime? acceptedAt)
        {
            ContractId = contractId;
            AcceptedAt = acceptedAt;
        }
    }
}
