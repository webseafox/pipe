using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums
{
    public enum StatementType
    {
        [Description("Todos")]
        All = 0,
        [Description("Entrada")]
        Deposit = 1,
        [Description("Saída")]
        Outflow = 2
    }
}
