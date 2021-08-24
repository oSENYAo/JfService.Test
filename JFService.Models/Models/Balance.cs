using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Models.Models
{
    public class Balance
    {
        public int id { get; set; }
        public int account_id { get; set; }
        public string period { get; set; }
        public decimal in_balance { get; set; }
        public decimal calculation { get; set; }
        public DateTime DateTimePeriod { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
    }

    public class Root
    {
        public List<Balance> balance { get; set; }
    }
}
