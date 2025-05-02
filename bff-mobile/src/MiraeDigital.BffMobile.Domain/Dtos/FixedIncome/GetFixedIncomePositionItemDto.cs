using System;

namespace MiraeDigital.BffMobile.Domain.Dtos.FixedIncome
{
    public class GetFixedIncomePositionItemDto
    {
        public int InvestorId { get; set; }
        public string InvestorName { get; set; }
        public string InvestorDocument { get; set; }
        public string ProductMinicomId { get; set; }
        public string ProductName { get; set; }
        public DateTime EmissionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal CustomerFee { get; set; }
        public string CodeIf { get; set; }
        public string IndexTypeDescription { get; set; }
        public decimal UnitaryPrice { get; set; }
        public string IssuerCode { get; set; }
        public string IssuerName { get; set; }
        public string LiquidityTypeDescription { get; set; }
        public string Description { get; set; }
        public DateTime MovementDate { get; set; }
        public string ProductTypeDescription { get; set; }
        public bool IsRedemptionAvailable { get; set; }
        public PositionSyntheticTotalDto Total { get; set; }
        public PositionSyntheticTotalRedemptionDto Redemption { get; set; }
        public PositionSyntheticTotalBlockedDto Blocked { get; set; }
    }
}
