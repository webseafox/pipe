using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Dtos.Statements
{
    public class StatementsDateGroupDto
    {
        public DateTime LiquidationDate { get; set; }
        public int QuantityTransactions { get; set; }
        public double AvailableBalance { get; set; }
        public IEnumerable<GetStatementDto> Statements { get; set; }
    }
}
