using JFService.Data.Data;
using JFService.Models.Models;
using JFService.Shared.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JFService.Data.EntityFramework
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Payment> FirstPaymentOrderBy(int accId)
        {
            return await _context.Payments.OrderBy(x => x.date).FirstOrDefaultAsync(x => x.account_id == accId);
        }
        public async Task<Payment> FirstPaymentOrderByDescending(int accId)
        {
            return await _context.Payments.OrderByDescending(x => x.date).FirstOrDefaultAsync(x => x.account_id == accId);
        }

        public async Task<Payment> GetPaymentByaccId(int accId)
        {
            return  await _context.Payments.FirstOrDefaultAsync(x => x.account_id == accId);
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return  await _context.Payments.FirstOrDefaultAsync(x => x.id == id);
        }
    }
}
