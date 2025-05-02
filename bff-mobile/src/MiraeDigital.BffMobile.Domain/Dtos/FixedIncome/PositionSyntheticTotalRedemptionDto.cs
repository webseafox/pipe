namespace MiraeDigital.BffMobile.Domain.Dtos.FixedIncome
{
    public class PositionSyntheticTotalRedemptionDto
    {
        public decimal TotalRedemptionQuantity { get; set; }
        public decimal TotalRedemptionAppliedAmount { get; set; }
        public decimal TotalCurrentRedemptionAmount { get; set; }
        public decimal TotalIofRedemptionAmount { get; set; }
        public decimal TotalIrRedemptionAmount { get; set; }
        public decimal TotalNetRedemptionAmount { get; set; }
    }
}
