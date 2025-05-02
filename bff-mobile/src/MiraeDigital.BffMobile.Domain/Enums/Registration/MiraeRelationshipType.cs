using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    [DefaultValue(None)]
    public enum MiraeRelationshipType
    {
        None = 0,

        [Description("Administradores, empregados, operadores e prepostos da corretora")]
        Employee = 1,

        [Description("Agentes Autônomos")]
        Freelancer = 2,

        [Description(
            "Demais profissionais que mantenham, com a corretora, contrato de prestação de serviços diretamente relacionados à atividade de intermediação"
        )]
        Contractor = 3,

        [Description("Sócios ou acionistas da corretora, pessoas físicas")]
        Partner = 4,

        [Description(
            "Os sócios, acionistas, e sociedades controladas direta ou indiretamente pela corretora, pessoas jurídicas, excetuadas as instituições financeiras e as instituições a elas equiparadas"
        )]
        Holding = 5,

        [Description(
            "Cônjuge ou companheiro e filhos menores das pessoas mencionadas nos incisos 1 a 5"
        )]
        MiraeRelatedFamilyMember = 6
    }
}
