using MiraeDigital.BffMobile.Domain.Enums.Registration;
using System;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    /// <summary>
    /// [11/2024] Compatible with query endpoints by:
    /// Jwt Token, Document, CustomerId, InvestorId
    /// </summary>
    public class GetCustomerResponse
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public long Document { get; set; }
        public int DocumentType { get; set; }
        public long CustomerId { get; set; }
        public long McbCode { get; set; }
        public long InvestorId { get; set; }
        public int? InvestorDigit { get; set; }
        public bool NonResidentInvestor { get; set; }
        public Gender? Gender { get; set; }
        public string GenderDescription { get; set; }
        public Naturalization? Naturalization { get; set; }
        public string NaturalizationDescription { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string MaritalStatusDescription { get; set; }
        public SuitabilityProfile? Suitability { get; set; }
        public string SuitabilityDescription { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }
        public string CountryExternalCode { get; set; }
        public string Country { get; set; }
        public string StateExternalCode { get; set; }
        public string State { get; set; }
        public string CityExternalCode { get; set; }
        public string City { get; set; }
        public long? NumberNif { get; set; }
        public bool IsPoliticalyExposed { get; set; }
        public CustomerStatus CustomerStatus { get; set; }
        public string CustomerStatusDescription { get; set; }
        public DateTime LastRecordUpdatedDate { get; set; }
        public MiraeRelationshipType MiraeRelationshipType { get; set; }
        public int AdvisorCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AccountCreatedAt { get; set; }
        public QualifiedInvestorContractResponse QualifiedInvestorContract { get; set; }
        public IEnumerable<BankAccountResponse> BankAccounts { get; set; }
        public DocumentIdentityResponse DocumentIdentity { get; set; }
        public SpouseResponse Spouse { get; set; }
        public AddressResponse Residence { get; set; }
        public AddressResponse Comercial { get; set; }
        public ContactResponse Contacts { get; set; }
        public ContractResponse IntermediationContract { get; set; }
        public ContractResponse IntermediantionAdhesionTerm { get; set; }
        public ContractResponse IntermediationPersonalDataConsentTerm { get; set; }
        public ContractResponse FreelanceAgentContract { get; set; }
        public ProfessionalInformationResponse ProfessionalInformation { get; set; }
        public FinancialInformationResponse FinancialInformation { get; set; }
        public LegalRepresentativeResponse LegalRepresentative { get; set; }
        public UsPersonDeclarationResponse UsPersonDeclarations { get; set; }
        public IEnumerable<CustomerHistoryResponse> CustomerHistory { get; set; }
    }
}
