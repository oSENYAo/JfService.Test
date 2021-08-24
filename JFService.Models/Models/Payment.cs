using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Models.Models
{
    public class Payment
    {
        public int id { get; set; }
        public int account_id { get; set; }
        public DateTime date { get; set; }
        public decimal sum { get; set; }
        public string payment_guid { get; set; }
        public Balance BalanceId { get; set; }
    }

}
