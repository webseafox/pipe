namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomer
{
    public class FinancialInformationResponse
    {
        public double MonthlyIncome { get; set; }
        public string MonthlyIncomeDescription { get; set; }
        public double TotalInvestiments { get; set; }
        public double AnnualIncome { get; set; }
        public double RealEstate { get; set; }
        public double OtherAssets { get; set; }
    }
}
