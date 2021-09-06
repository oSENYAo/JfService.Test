using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JfService.Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    in_balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    calculation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTimePeriod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payment_guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payments_Balances_BalanceIdid",
                        column: x => x.BalanceIdid,
                        principalTable: "Balances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BalanceIdid",
                table: "Payments",
                column: "BalanceIdid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Balances");
        }
    }
}
