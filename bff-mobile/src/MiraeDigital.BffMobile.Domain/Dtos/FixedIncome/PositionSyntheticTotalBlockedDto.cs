namespace MiraeDigital.BffMobile.Domain.Dtos.FixedIncome
{
    public class PositionSyntheticTotalBlockedDto
    {
        public decimal TotalBlockedQuantity { get; set; }
        public decimal TotalBlockedAppliedAmount { get; set; }
        public decimal TotalCurrentBlockedAmount { get; set; }
        public decimal TotalIofBlockedAmount { get; set; }
        public decimal TotalIrBlockedAmount { get; set; }
        public decimal TotalNetBlockedAmount { get; set; }
    }
}
