using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums
{
    public enum SignatureResult
    {
        [Description("Assinatura eletrônica inserida.")]
        Created = 1,

        [Description("Assinatura eletrônica resetada.")]
        Reseted = 2,

        [Description("Formato inválido.")]
        InvalidFormat = 3,

        [Description("Assinatura eletrônica igual última.")]
        SameAsLast = 4,

        [Description("Assinatura validada com sucesso.")]
        Validated = 5,

        [Description("Assinatura inválida.")]
        Invalid = 6
    }
}
