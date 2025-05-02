using MiraeDigital.BffMobile.Domain.Enums.Registration;
using MiraeDigital.BffMobile.Domain.Http.Authentication.Response.GetUser;
using MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer;
using MiraeDigital.BffMobile.Domain.Http.Suitability.Responses.GetSuitabilityCurrent;
using MiraeDigital.Lib.Application.UseCases;
using System;

namespace MiraeDigital.BffMobile.Application.UseCases.Account.GetBasicInformation
{
    public class GetBasicInformationOutput : IUseCaseOutput
    {
        public string Name { get; set; }
        public int Bank { get; } = 447;
        public int BankBranch { get; } = 1;
        public long Account { get; set; }
        public int? Digit { get; set; }
        public long McbCode { get; set; }
        public SuitabilityProfile Suitability { get; set; }
        public string SuitabilityDescription { get; set; }
        public DateTime LastUpdateSuitability { get; set; }
        public CustomerStatus CustomerStatus { get; set; }
        public string CustomerStatusDescription { get; set; }
        public bool HasEletronicSignature { get; set; }
        public QualifiedInvestorContractItem QualifiedInvestorContract { get; set; }

        public GetBasicInformationOutput() { }

        public static GetBasicInformationOutput Create(GetCustomerResponse customerResponse, SuitabilityCurrentResponse suitabilityResponse, UserResponse userResponse)
        {
            GetBasicInformationOutput output = new GetBasicInformationOutput();

            output.Name = customerResponse.Name;
            output.McbCode = customerResponse.McbCode;

            output.CustomerStatus = customerResponse.CustomerStatus;
            output.CustomerStatusDescription = customerResponse.CustomerStatusDescription;
            output.QualifiedInvestorContract = customerResponse.QualifiedInvestorContract != null ? 
                        new QualifiedInvestorContractItem(customerResponse.QualifiedInvestorContract.ContractId, customerResponse.QualifiedInvestorContract.AcceptedAt) : null;

            output.Account = customerResponse.InvestorId;
            output.Digit = customerResponse.InvestorDigit;
            
            output.HasEletronicSignature = userResponse.HasEletronicSignature;

            output.Suitability = suitabilityResponse.SuitabilityProfile;
            output.SuitabilityDescription = suitabilityResponse.Classification;
            output.LastUpdateSuitability = suitabilityResponse.CreatedAt;
            
            return output;
        }
    }
}
