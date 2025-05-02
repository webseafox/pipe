using System;

namespace MiraeDigital.BffMobile.Domain.Dtos.FixedIncome
{
    public class OrdersDto
    {
        public long OrderId { get; set; }
        public long IssuerId { get; set; }
        public long IssuerMinicomCode { get; set; }
        public string IssuerName { get; set; }
        public string ProductName { get; set; }
        public long InvestorId { get; set; }
        public int InvestorDigit { get; set; }
        public decimal RequestedAmount { get; set; }
        public string RequestedAmountFormatted { get; set; }
        public int OperationStatus { get; set; }
        public string OperationStatusDescription { get; set; }
        public DateTime OperationDate { get; set; }
        public int OperationType { get; set; }
        public string OperationTypeDescription { get; set; }
        public string ProductTypeDescription { get; set; }
        public decimal? CustomerFee { get; set; }
        public string CustomerFeeFormatted { get; set; }
        public decimal Quantity { get; set; }
        public string IndexTypeDescription { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int DeadlineDays { get; set; }
        public decimal ProductUnitaryPrice { get; set; }
        public string LiquidityTypeDescription { get; set; }
        public bool IsFirstOrder { get; set; }
        public decimal? BaseFee { get; set; }
        public string BaseFeeFormatted { get; set; }
        public string CodeIf { get; set; }
        public string IntegrationOrderMessageError { get; }
    }
}
