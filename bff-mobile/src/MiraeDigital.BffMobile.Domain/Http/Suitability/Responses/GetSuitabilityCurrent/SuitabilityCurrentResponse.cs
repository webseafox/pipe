using MiraeDigital.BffMobile.Domain.Enums.Registration;
using MiraeDigital.BffMobile.Domain.Extensions.Suitability;
using System;

namespace MiraeDigital.BffMobile.Domain.Http.Suitability.Responses.GetSuitabilityCurrent;

/// <summary>
/// [11/2024] Compatible with query endpoints by:
/// Jwt Token, Document, SuitabilityId
/// </summary>
public class SuitabilityCurrentResponse
{
    public long SuitabilityId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public long QuizId { get; set; }
    public string Classification { get; set; }
    public int ClassificationPoints { get; set; }
    public int Score { get; set; }

    public SuitabilityProfile SuitabilityProfile => Classification.GetSuitabilityProfileByClassification();
}
