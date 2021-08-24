using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Service
{
    public class YearService
    {
        public DateTime periodYear { get; set; }             // Год
        public decimal YearStartingBalance { get; set; }   // начальный баланс за период
        public decimal YearAssessed { get; set; }          // начислено за период
        public decimal YearPaid { get; set; }              // потрачено за период
        public decimal YearFinalBalance { get; set; }      // осталось за период

    }
}
