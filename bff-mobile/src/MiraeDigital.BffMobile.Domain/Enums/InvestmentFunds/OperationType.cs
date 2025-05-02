using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.InvestmentFunds
{
    [DefaultValue(Aplication)]
    public enum OperationType
    {
        [Description("Aplicação")]
        Aplication = 1,

        [Description("Resgate Total")]
        FullApplicationRedemption = 2,

        [Description("Resgate Parcial")]
        PartialApplicationRedemption = 3
    }
}
