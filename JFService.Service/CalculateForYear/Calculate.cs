using JFService.Data.Data;
using JFService.Service.CalculateForYear;
using JFService.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFService.Service
{
    public class Calculate : ICalculate<YearService, MonthService, QuarterService>
    {
        private readonly AppDbContext _context;
        private readonly FindDates _find;

        public Calculate(AppDbContext context, FindDates find)
        {
            _context = context;
            _find = find;
        }

        public async Task<List<MonthService>> Monts(int accId)
        {
            var firstAndLastDate = await _find.FindDate(accId);

            List<MonthService> months = new List<MonthService>();

            int i = 1;
            while (firstAndLastDate.firstYear <= firstAndLastDate.lastYear)
            {
                MonthService month = new MonthService();
                var result = await _context.Balances.Where(x => x.DateTimePeriod.Month == firstAndLastDate.firstYear.Month).ToListAsync();
                month.MonthAssessed = result.Select(x => x.calculation).Sum();
                month.MonthStartingBalance = result.Select(x => x.in_balance).Sum();
                month.periodMonth = firstAndLastDate.firstYear;

                var results = await _context.Payments.Where(x => x.date.Month == firstAndLastDate.firstYearPayments.Month).ToListAsync();
                month.MonthPaid = results.Select(x => x.sum).Sum();
                month.MonthFinalBalance = month.MonthStartingBalance + (month.MonthAssessed - month.MonthPaid);
                months.Add(month);
                firstAndLastDate.firstYear = firstAndLastDate.dt.AddMonths(i);
                firstAndLastDate.firstYearPayments = firstAndLastDate.dt2.AddMonths(i);
                i++;
            }
            return months;

        }

        public async Task<List<QuarterService>> Quarters(int accId)
        {
            var firstAndLastDate = await _find.FindDate(accId);

            List<QuarterService> quarters = new List<QuarterService>();

            int i = 3;
            while (firstAndLastDate.firstYear <= firstAndLastDate.lastYear)
            {
                QuarterService quarter = new QuarterService();
                var result = await _context.Balances.Where(x => x.DateTimePeriod.Month == firstAndLastDate.firstYear.Month).ToListAsync();
                quarter.QuarterAssessed = result.Select(x => x.calculation).Sum();
                quarter.QuarterStartingBalance = result.Select(x => x.in_balance).Sum();
                quarter.periodQuarter = firstAndLastDate.firstYear;

                var results = await _context.Payments.Where(x => x.date.Month == firstAndLastDate.firstYearPayments.Month).ToListAsync();
                quarter.QuarterPaid = results.Select(x => x.sum).Sum();
                quarter.QarterFinalBalance = quarter.QuarterStartingBalance + (quarter.QuarterAssessed - quarter.QuarterPaid);
                quarters.Add(quarter);
                firstAndLastDate.firstYear = firstAndLastDate.dt.AddMonths(i);
                firstAndLastDate.firstYearPayments = firstAndLastDate.dt2.AddMonths(i);
                i += 3;
            }
            return quarters;

        }

        public async Task<List<YearService>> Years(int accId)
        {
            var firstAndLastDate = await _find.FindDate(accId);

            List<YearService> years = new List<YearService>();

            int i = 1;
            while (firstAndLastDate.firstYear <= firstAndLastDate.lastYear)
            {
                YearService ys = new YearService();
                var result = await _context.Balances.Where(x => x.DateTimePeriod.Year == firstAndLastDate.firstYear.Year).ToListAsync();
                ys.YearAssessed = result.Select(x => x.calculation).Sum();
                ys.YearStartingBalance = result.Select(x => x.in_balance).Sum();
                ys.periodYear = firstAndLastDate.firstYear;

                var results = await _context.Payments.Where(x => x.date.Year == firstAndLastDate.firstYearPayments.Year).ToListAsync();
                ys.YearPaid = results.Select(x => x.sum).Sum();
                ys.YearFinalBalance = ys.YearStartingBalance + (ys.YearAssessed - ys.YearPaid);
                years.Add(ys);
                firstAndLastDate.firstYear = firstAndLastDate.dt.AddYears(i);
                firstAndLastDate.firstYearPayments = firstAndLastDate.dt2.AddYears(i);
                i++;
            }
            return years;
        }

    }
}
