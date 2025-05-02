using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    [DefaultValue(None)]
    public enum MaritalStatus
    {
        [Description("NÃO INFORMADO")]
        None = 0,

        [Description("SOLTEIRO(A)")]
        Single = 1,

        [Description("SEPARADO(A) JUDICIALMENTE")]
        JudicialSepárated = 2,

        [Description("VIÚVO(A)")]
        Widower = 3,

        [Description("DIVORCIADO(A)")]
        Divorced = 4,

        [Description("CASADO(A)")]
        Married = 5,

        [Description("CASADO(A) COM BRASILEIRO(A) NATURALIZADO")]
        MarriedWithNaturalized = 6,

        [Description("CASADO(A) COM ESTRANGEIRO(A)")]
        MarriedWithForeigner = 7,

        [Description("UNIÃO ESTÁVEL")]
        CommonLawMarriage = 8
    }
}
