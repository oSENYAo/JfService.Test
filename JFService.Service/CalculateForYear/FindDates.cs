using JFService.Data;
using System;
using System.Threading.Tasks;

namespace JFService.Service.CalculateForYear
{
    public class FindDates
    {
        private readonly DataManager _manager;

        public FindDates(DataManager manager)
        {
            _manager = manager;
        }
        public async Task<FirstAndLastDate> FindDate(int accId)
        {
            // Balances
            var firstbalans = await _manager.BalanceRepository.FirstOrderBy(accId);
            var lastBalans = await _manager.BalanceRepository.FirstOrderByDescending(accId);

            // Payments
            var firstPayments = await _manager.PaymetRepository.FirstPaymentOrderBy(accId);
            var lastPayments = await _manager.PaymetRepository.FirstPaymentOrderByDescending(accId);

            // dates
            var firstYearPayments = firstPayments.date;
            var lastYearPayments = lastPayments.date;

            //initial balance
            var initBalance = firstbalans.in_balance;


            var firstYear = firstbalans.DateTimePeriod;
            var lastYear = lastBalans.DateTimePeriod;
            DateTime dt = firstYear;
            DateTime dt2 = firstYearPayments;

            FirstAndLastDate firstAndLastDate = new FirstAndLastDate
            {
                firstYear = firstYear,
                lastYear = lastYear,
                firstYearPayments = firstYearPayments,
                lastYearPayments = lastYearPayments,
                dt = dt,
                dt2 = dt2,
                InitialBalance = initBalance
            };

            return firstAndLastDate;
        }
    }
}
