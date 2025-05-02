using System;
using System.Collections.Generic;

namespace MiraeDigital.BffMobile.Domain.Http.FixedIncomePositon.Responses.GetFixedIncomePositionRedemptionAvailable
{
    public class GetFixedIncomePositionRedemptionAvailableResponse
    {
        public IEnumerable<GetFixedIncomePositionRedemptionAvailableItem> Positions { get; set; }
    }

    public class GetFixedIncomePositionRedemptionAvailableItem
    {
        public string ProductMinicomId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string IssuerMinicomId { get; set; }
        public string IssuerName { get; set; }
        public decimal CustomerFee { get; set; }
        public string IndexTypeDescription { get; set; }
        public DateTime EmissionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime MovementDate { get; set; }
        public decimal AmountRedemption { get; set; }
        public decimal CurrentValueRedemption { get; set; }
        public decimal NetValueRedemption { get; set; }
        public string LiquidityTypeDescription { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal IrValueRedemption { get; set; }
        public decimal IofValueRedemption { get; set; }
        public string CodeIf { get; set; }
        public decimal RedemptionQuantity { get; set; }
    }
}
