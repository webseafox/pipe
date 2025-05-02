using System.ComponentModel;

namespace MiraeDigital.BffMobile.Domain.Enums.Registration
{
    [DefaultValue(None)]
    public enum Naturalization
    {
        None = 0,

        [Description("BRASILEIRO NATO")]
        Native = 1,

        [Description("BRASILEIRO NATURALIZADO")]
        Naturalized = 2,

        [Description("ESTRANGEIRO")]
        Foreigner = 3
    }
}
