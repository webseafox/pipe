using System;

namespace MiraeDigital.BffMobile.Domain.Dtos.InvestmentFunds.GetOrders
{
    public class GetOrdersDto
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public int CustomerSuitability { get; set; }
        public string CustomerSuitabilityDescription { get; set; }
        public int FundSuitability { get; set; }
        public string FundSuitabilityDescription { get; set; }
        public long InvestorId { get; set; }
        public int InvestorDigit { get; set; }
        public double RequestedAmount { get; set; }
        public int OperationType { get; set; }
        public string OperationTypeDescription { get; set; }
        public int InputChannel { get; set; }
        public string InputChannelDescription { get; set; }
        public int OperationStatus { get; set; }
        public string OperationStatusDescription { get; set; }
        public bool MismatchRisk { get; set; }
        public bool SignedMismatchRiskTerm { get; set; }
        public long InvestmentFundId { get; set; }
        public string InvestmentFundName { get; set; }
        public string InvestmentFundDocument { get; set; }
        public bool QualifiedInvestorFund { get; set; }
        public bool QualifiedInvestorCustomer { get; set; }
        public long OrderManagerDocument { get; set; }
        public string OrderManagerName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime MovementDate { get; set; }
        public DateTime QuoteDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public int OrderReceivedBy { get; set; }
        public string Note { get; set; }
        public string FilePath { get; set; }
        public double CustomerAmountAvailable { get; set; }
        public string RefusedMessage { get; set; }
        public double RequestedGrossAmount { get; set; }
    }
}
