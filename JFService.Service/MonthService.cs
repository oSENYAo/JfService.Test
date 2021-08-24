using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Service
{
    public class MonthService
    {
        public DateTime periodMonth { get; set; }             // Год
        public decimal MonthStartingBalance { get; set; }   // начальный баланс за период
        public decimal MonthAssessed { get; set; }          // начислено за период
        public decimal MonthPaid { get; set; }              // потрачено за период
        public decimal MonthFinalBalance { get; set; }      // осталось за период
    }
}
