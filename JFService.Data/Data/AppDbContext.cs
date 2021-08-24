using Microsoft.EntityFrameworkCore;
using JFService.Models.Models;
namespace JFService.Data.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
