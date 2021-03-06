// <auto-generated />
using System;
using JFService.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JfService.Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JFService.Models.Models.Balance", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimePeriod")
                        .HasColumnType("datetime2");

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<decimal>("calculation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("in_balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("period")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("JFService.Models.Models.Payment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BalanceIdid")
                        .HasColumnType("int");

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("payment_guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("sum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("BalanceIdid");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("JFService.Models.Models.Payment", b =>
                {
                    b.HasOne("JFService.Models.Models.Balance", "BalanceId")
                        .WithMany("Payments")
                        .HasForeignKey("BalanceIdid");

                    b.Navigation("BalanceId");
                });

            modelBuilder.Entity("JFService.Models.Models.Balance", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
