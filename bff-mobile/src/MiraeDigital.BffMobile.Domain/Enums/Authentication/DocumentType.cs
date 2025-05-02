using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Authentication
{
    public enum DocumentType
    {
        None = 0,

        [Description("CPF")]
        Person = 1,

        [Description("CNPJ")]
        Legal = 2,
    }
}
