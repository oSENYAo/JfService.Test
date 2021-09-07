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
            var initBalans = firstAndLastDate.InitialBalance;
            
            while (firstAndLastDate.firstYear <= firstAndLastDate.lastYear)
            {
                MonthService month = new MonthService();
                var yearCount = await _context.Balances.Where(x => x.DateTimePeriod.Month == firstAndLastDate.firstYear.Month).ToListAsync();
                if (i == 1)
                    month.MonthStartingBalance = initBalans;
                else
                    month.MonthStartingBalance = months.Select(x => x.MonthFinalBalance).LastOrDefault();
                month.MonthAssessed = yearCount.Where(x => x.DateTimePeriod == firstAndLastDate.firstYear).Select(x => x.calculation).FirstOrDefault();
                month.periodMonth = firstAndLastDate.firstYear;

                var payment = await _context.Payments.Where(x => x.date.Month == firstAndLastDate.firstYear.Month && x.date.Year == firstAndLastDate.firstYear.Year).ToListAsync();
                
                if (payment != null)
                {
                    month.MonthPaid = payment.Sum(x => x.sum);
                    month.MonthFinalBalance = month.MonthStartingBalance + (month.MonthAssessed - month.MonthPaid);
                }
                else
                    month.MonthFinalBalance = month.MonthStartingBalance + month.MonthAssessed;

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
            var initBalans = firstAndLastDate.InitialBalance;

            while (firstAndLastDate.firstYear <= firstAndLastDate.lastYear)
            {
                QuarterService quarter = new QuarterService();
                var yearCount = await _context.Balances.Where(x => x.DateTimePeriod.Month == firstAndLastDate.firstYear.Month).ToListAsync();
                
                if (i == 3)
                    quarter.QuarterStartingBalance = initBalans;
                else
                    quarter.QuarterStartingBalance = quarters.Select(x => x.QarterFinalBalance).LastOrDefault();
                    
                quarter.QuarterAssessed = yearCount.Where(x => x.DateTimePeriod >= firstAndLastDate.firstYear).Take(3).Select(x => x.calculation).Sum();
                quarter.periodQuarter = firstAndLastDate.firstYear;

                var payments = _context.Payments
                    .Where(x => x.date >= firstAndLastDate.firstYear).Take(3).Select(x => x.sum).Sum();
                
                if (true)
                {
                    quarter.QuarterPaid = payments;
                    quarter.QarterFinalBalance = quarter.QuarterStartingBalance + (quarter.QuarterAssessed - quarter.QuarterPaid);
                }
                //else
                //    quarter.QarterFinalBalance = quarter.QuarterStartingBalance + quarter.QuarterAssessed;

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
            var initBalans = firstAndLastDate.InitialBalance;

            while (firstAndLastDate.firstYear <= firstAndLastDate.lastYear)
            {
                YearService ys = new YearService();
                var yearCount = await _context.Balances.Where(x => x.DateTimePeriod.Year == firstAndLastDate.firstYear.Year).ToListAsync();
                
                if (i == 1)
                    ys.YearStartingBalance = initBalans;
                else
                    ys.YearStartingBalance = years.Select(x => x.YearFinalBalance).LastOrDefault();

                ys.YearAssessed = yearCount.Where(x => x.DateTimePeriod == firstAndLastDate.firstYear).Select(x => x.calculation).FirstOrDefault();
                ys.periodYear = firstAndLastDate.firstYear;

                var payments = await _context.Payments
                    .Where(x => x.date.Year == firstAndLastDate.firstYearPayments.Year && x.date.Month == firstAndLastDate.firstYearPayments.Month)
                    .ToListAsync();
                if (payments != null)
                {
                    ys.YearPaid = payments.Sum(x => x.sum);
                    ys.YearFinalBalance = ys.YearStartingBalance + (ys.YearAssessed - ys.YearPaid);
                }
                else
                    ys.YearFinalBalance = ys.YearStartingBalance + ys.YearAssessed;

                years.Add(ys);
                firstAndLastDate.firstYear = firstAndLastDate.dt.AddYears(i);
                firstAndLastDate.firstYearPayments = firstAndLastDate.dt2.AddYears(i);
                i++;
            }
            return years;
        }

    }
}
