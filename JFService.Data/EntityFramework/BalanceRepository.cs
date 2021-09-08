using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using JFService.Data.Data;
using JFService.Models.Models;
using JFService.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JFService.Data.EntityFramework
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly AppDbContext _context;

        public BalanceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Balance> FirstOrderBy(int accId)
        {
            return await _context.Balances.OrderBy(x => x.DateTimePeriod).FirstOrDefaultAsync(x => x.account_id == accId);
        }
        public async Task<Balance> FirstOrderByDescending(int accId)
        {
            return await _context.Balances.OrderByDescending(x => x.DateTimePeriod).FirstOrDefaultAsync(x => x.account_id == accId);
        }

        public async Task<Balance> GetBalanceByaccId(int accId)
        {
            return await _context.Balances.FirstOrDefaultAsync(x => x.account_id == accId);
        }

        public async Task<Balance> GetBalanceById(int id)
        {
            return await _context.Balances.FirstOrDefaultAsync(x => x.id == id);
        }
        public async Task UpdateDatabase()
        {
            string jsonPayment = System.IO.File.ReadAllText(@"C:\Users\Razrab\source\repos\JfService.Test\JFService.Data\Source\payment_202105270827.json");
            string jsonBalance = System.IO.File.ReadAllText(@"C:\Users\Razrab\source\repos\JfService.Test\JFService.Data\Source\balance_202105270825.json");


            List<Payment> payments = new List<Payment>();
            payments = JsonConvert.DeserializeObject<List<Payment>>(jsonPayment);

            Root root = new Root();
            root = JsonConvert.DeserializeObject<Root>(jsonBalance);

            foreach (var item in root.balance)
            {
                DateTime dt;
                DateTime.TryParseExact(item.period, "yyyyMM", CultureInfo.InvariantCulture,
                  DateTimeStyles.None, out dt);
                item.DateTimePeriod = dt;
            }
            var result = root.balance.OrderByDescending(x => x.DateTimePeriod).LastOrDefault();
            var result2 = await _context.Balances.OrderByDescending(x => x.DateTimePeriod).LastOrDefaultAsync();


            var payResult = payments.OrderByDescending(x => x.date).LastOrDefault();
            var payResult2 = await _context.Payments.OrderByDescending(x => x.date).LastOrDefaultAsync();

            if (result2 != null && payResult2 != null)
            {
                if (result.DateTimePeriod != result2.DateTimePeriod)
                    await _context.Balances.AddRangeAsync(root.balance);

                if (payResult.date != payResult2.date)
                    await _context.Payments.AddRangeAsync(payments);
            }
            else
            {
                await _context.Balances.AddRangeAsync(root.balance);
                await _context.Payments.AddRangeAsync(payments);
            }

            await _context.SaveChangesAsync();
        }
    }
}
