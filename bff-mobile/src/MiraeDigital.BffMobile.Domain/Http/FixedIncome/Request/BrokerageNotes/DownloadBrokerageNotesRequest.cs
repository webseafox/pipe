
using System;

namespace MiraeDigital.BffMobile.Domain.Http.FixedIncome.Request.BrokerageNotes
{
    public class DownloadBrokerageNotesRequest
    {
        public long InvestorId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
