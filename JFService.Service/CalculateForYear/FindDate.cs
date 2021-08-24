using JFService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


            var firstYearPayments = firstPayments.date;
            var lastYearPayments = lastPayments.date;



            var firstYear = firstbalans.DateTimePeriod;
            var lastYear = lastBalans.DateTimePeriod;
            DateTime dt = firstYear;
            DateTime dt2 = firstYearPayments;
            FirstAndLastDate firstAndLastDate = new FirstAndLastDate
            {
                firstYear = firstYear,
                lastYear = lastYear,
                firstYearPayments = firstYearPayments,
                dt = dt,
                dt2 = dt2
            };

            return firstAndLastDate;
        }
    }
}
