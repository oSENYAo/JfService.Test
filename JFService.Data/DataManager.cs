using JFService.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Data
{
    public class DataManager
    {
        public IPaymentRepository PaymetRepository { get; set; }
        public IBalanceRepository BalanceRepository { get; set; }

        public DataManager(IPaymentRepository paymetRepository, IBalanceRepository balanceRepository)
        {
            PaymetRepository = paymetRepository;
            BalanceRepository = balanceRepository;
        }
    }
}
