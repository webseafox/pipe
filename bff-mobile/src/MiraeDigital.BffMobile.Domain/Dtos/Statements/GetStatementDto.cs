using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiraeDigital.BffMobile.Domain.Dtos.Statements
{
    public class GetStatementDto
    {
        public string RegisterDate { get; set; }
        public string LiquidationDate { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int Type => Value < 0 ? 0 : 1;
    }
}
