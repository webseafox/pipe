using MiraeDigital.BffMobile.Domain.Enums.Registration;

namespace MiraeDigital.BffMobile.Domain.Extensions.Suitability
{
    public static class SuitabilityExtensions
    {
        public static SuitabilityProfile GetSuitabilityProfileByClassification(this string category) => category switch
        {
            "CONSERVADOR" => SuitabilityProfile.Conservative,
            "MODERADO" => SuitabilityProfile.Moderate,
            "AGRESSIVO" => SuitabilityProfile.Aggressive,
            _=> SuitabilityProfile.None
        };

    }
}
