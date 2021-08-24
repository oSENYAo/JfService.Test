using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Service
{
    public class QuarterService
    {
        public DateTime periodQuarter { get; set; }             // Год
        public decimal QuarterStartingBalance { get; set; }   // начальный баланс за период
        public decimal QuarterAssessed { get; set; }          // начислено за период
        public decimal QuarterPaid { get; set; }              // потрачено за период
        public decimal QarterFinalBalance { get; set; }      // осталось за период

    }
}
