using JFService.Shared.Abstract;

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
