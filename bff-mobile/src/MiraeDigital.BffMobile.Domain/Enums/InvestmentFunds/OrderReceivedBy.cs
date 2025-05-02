using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.InvestmentFunds
{
    public enum OrderReceivedBy
    {
        [Description("Email")]
        Email = 1,
        [Description("Telefone")]
        Phone = 2,
        [Description("Outros")]
        Others = 3
    }
}
