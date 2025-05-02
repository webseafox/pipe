namespace MiraeDigital.BffMobile.Domain.Enums.CustomerTwoFactor
{
    public enum TokenMethod
    {
        Email = 0,
        App = 1
    }

    public enum TokenMethodHotp
    {
        Email = TokenMethod.Email
    }

    public enum TokenMethodTotp
    {
        App = TokenMethod.App
    }
}
