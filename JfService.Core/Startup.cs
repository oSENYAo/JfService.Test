using JFService.Data;
using JFService.Data.Data;
using JFService.Data.EntityFramework;
using JFService.Service;
using JFService.Service.CalculateForYear;
using JFService.Shared;
using JFService.Shared.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace JfService.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectString = Configuration.GetConnectionString("DefaultConnection"); // i used Manage User secrets
            
            
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectString, x => x.MigrationsAssembly("JfService.Core")));
            services.AddTransient<IBalanceRepository, BalanceRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<DataManager>();
            var dbOptions = new DbContextOptions<AppDbContext>();
            var dataManager = new DataManager(new PaymentRepository(new AppDbContext(dbOptions)), new BalanceRepository(new AppDbContext(dbOptions)));
            var calculate = new Calculate(new AppDbContext(dbOptions), new FindDates(dataManager));
            services.AddTransient<FindDates>();
            services.AddTransient<ICalculate<YearService, MonthService, QuarterService>, Calculate>();
            
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
