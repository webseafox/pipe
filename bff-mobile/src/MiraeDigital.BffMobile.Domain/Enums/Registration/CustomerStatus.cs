using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    [DefaultValue(0)]
    public enum CustomerStatus
    {
        [Description("Status definition doesn't exist")]
        None = 0,
        Active = 1,
        BasicAccess = 2,
        PersonalInformaiton = 3,
        ResidencialInformation = 4,
        ProfessionalInformation = 5,
        FinancialInformation = 6,
        SuitabilityInformation = 7,
        CompletedRegistration = 8,
        Approved = 9,
        DocumentAlreadyExists = 10,
        EmailAlreadyExistis = 11,
        WithError = 12,

        [Description("Client not found")]
        NotFound = 13,
        BankAccountInformation = 14,
        DeclarationInformation = 15,
        LegalRepresentativeInformation = 16,
        UsPersonInformation = 17,
        IntermediationContractAccepted = 18,
        PendingApproval = 19,
        Inactive = 20,
        DocumentUploadPending = 21,
        PersonalData = 22,
        Document = 23
    }
}
