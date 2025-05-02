using System;


namespace MiraeDigital.BffMobile.Domain.Http.Registration.Response.GetCustomerAccounts
{
    public class CustomerAccountItemResponse
    {
        public string Name { get; set; }
        public CustomerAccountContactsItemResponse Contacts { get; set; }
        public long McbCode { get; set; }
        public long InvestorId { get; set; }
        public int? InvestorDigit { get; set; }
        public long UserId { get; set; }
        public long Document { get; set; }
        public int CustomerStatus { get; set; }
        public DateTime LastRecordUpdatedDate { get; set; }
    }
}
