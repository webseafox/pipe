using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.BrokerageInvoice
{
    public enum OriginType
    {
        [Description("BMFBovespa")]
        BmfBovespa,
        [Description("AluguelDeAcoes")]
        StockLease,
        [Description("RendaFixa")]
        FixedIncome
    }
}
