using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    public enum ContractType
    {
        None = 0,

        [Description("Contracto de intermediação")]
        IntermediationOperation = 1,

        [Description("Termo de Adesão ao Contrato de Intermediação")]
        IntermediationAdhesionTerm = 2,

        [Description("Termo de Consentimento para Tratamento de Dados Pessoais")]
        IntermediationPersonalDataConsentTerm = 3,

        [Description("Contrato de agente autônomo")]
        FreelanceAgentContract = 4,

        [Description("Investidor Qualificado")]
        QualifiedInvestorContract = 5,
    }
}
