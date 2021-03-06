using JFService.Data.Data;
using JFService.Service.CalculateForYear;
using JFService.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JFService.Service
{
    public class Calculate : ICalculate<YearService, MonthService, QuarterService>
    {
        private readonly AppDbContext _context;
        
        public Calculate(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthService>> Monts(int accId)
        {
            var firstAndLastDate = await FindDate(accId);

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
        public async Task<List<YearService>> ByYear()
        {

            var month = await Monts(808251);

            var grouped = month
                .OrderBy(x => x.periodMonth)
                .GroupBy(x => x.periodMonth.Year)
                .Select(x => new YearService
                {
                    periodYear = new System.DateTime(x.Key, 1, 1),
                    YearStartingBalance = x.FirstOrDefault().MonthStartingBalance,
                    YearAssessed = x.Sum(y => y.MonthAssessed),
                    YearPaid = x.Sum(y => y.MonthPaid),
                    YearFinalBalance = x.LastOrDefault().MonthFinalBalance
                }).ToList();


            return grouped;
        }
        public async Task<List<QuarterService>> Quarters(int accId)
        {
            var month = await Monts(accId);
           
            var quarter = month
                .OrderBy(x => x.periodMonth)
                .GroupBy(x => new {x.periodMonth.Year, quartal = NumberQuarters(x.periodMonth)})
                .Select(x => new QuarterService
                {
                    periodQuarter = new System.DateTime(x.Key.Year, SelectMonth(x.Key.quartal), 1),
                    QuarterStartingBalance = x.FirstOrDefault().MonthStartingBalance,
                    QuarterAssessed = x.Sum(y => y.MonthAssessed),
                    QuarterPaid = x.Sum(y => y.MonthPaid),
                    QarterFinalBalance = x.LastOrDefault().MonthFinalBalance
                }).ToList();

            return quarter;
        }

        public async Task<List<YearService>> Years(int accId)
        {
            var month = await Monts(accId);

            var grouped = month
                .OrderBy(x => x.periodMonth)
                .GroupBy(x => x.periodMonth.Year)
                .Select(x => new YearService
                {
                    periodYear = new System.DateTime(x.Key, 1, 1),
                    YearStartingBalance = x.FirstOrDefault().MonthStartingBalance,
                    YearAssessed = x.Sum(y => y.MonthAssessed),
                    YearPaid = x.Sum(y => y.MonthPaid),
                    YearFinalBalance = x.LastOrDefault().MonthFinalBalance
                }).ToList();


            return grouped;
        }
        public int NumberQuarters(DateTime date)
        {
            int quarter = (date.Month + 2) / 3;

            return quarter;
        }

        public static int SelectMonth(int number)
        {
            if (number == 4)
            {
                return 10;
            }
            else if (number == 3)
            {
                return 7;
            }
            else if(number == 2)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }

        public List<int> NameQuaretrs()
        {
            List<int> result = new List<int>();
            for (int i = 1; i < 5; i++)
            {
                result.Add(i);
            }
            return result;
        }
        public async Task<FirstAndLastDate> FindDate(int accId)
        {
            // Balances
            var firstbalans = await _context.Balances.OrderBy(x => x.DateTimePeriod).FirstOrDefaultAsync(x => x.account_id == accId);
            var lastBalans = await _context.Balances.OrderByDescending(x => x.DateTimePeriod).FirstOrDefaultAsync(x => x.account_id == accId);

            // Payments
            var firstPayments = await _context.Payments.OrderBy(x => x.date).FirstOrDefaultAsync(x => x.account_id == accId);
            var lastPayments = await _context.Payments.OrderByDescending(x => x.date).FirstOrDefaultAsync(x => x.account_id == accId);

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
