using System;

namespace MiraeDigital.BffMobile.Domain.Dtos.FinancialPosition
{
    public class PositionsItemDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime PositionDate { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public DateTime ConversionDate { get; set; }
        public decimal? QuotaValue { get; set; }
        public decimal? QuotaQuantity { get; set; }
        public decimal? CurrentQuota { get; set; }
        public decimal ApplicationValue { get; set; }
        public decimal Ir { get; set; }
        public decimal Iof { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetValue { get; set; }
    }
}
