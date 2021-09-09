using JFService.Data.Data;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JFService.Service
{
    public class Csv
    {
        private readonly AppDbContext _context;

        public Csv(AppDbContext context)
        {
            _context = context;
        }
        //public async Task CsvForMonth (int accId)
        //{
        //    Calculate calculate = new Calculate(_context);


        //    var monthCalculate = await calculate.Monts(accId);

        //    List<string> dates = new List<string>();
        //    string data = "";
        //    foreach (var month in monthCalculate)
        //    {
        //        data = 
        //            $"{month.periodMonth.ToString()}" +
        //            $",{month.MonthStartingBalance.ToString()}" +
        //            $",{month.MonthAssessed.ToString()}" +
        //            $",{month.MonthPaid.ToString()}" +
        //            $",{month.MonthFinalBalance.ToString()},";
        //        dates.Add(data);
        //    }
        //    string filePath = @"CsvForMonth.csv";
        //    await File.WriteAllTextAsync(filePath, data);
        //}
        public async Task CsvForMonth(int accId)
        {
            string filePath = @"CsvForMonth.csv";
            DataTable dt = new DataTable();

            Calculate calculate = new Calculate(_context);


            var monthCalculate = await calculate.Monts(accId);
                
            dt.Columns.Add(new DataColumn("Месяц", typeof(string)));
            dt.Columns.Add(new DataColumn("Баланс в начале периода", typeof(string)));
            dt.Columns.Add(new DataColumn("Начислено", typeof(string)));
            dt.Columns.Add(new DataColumn("Оплачено", typeof(string)));
            dt.Columns.Add(new DataColumn("Баланс в конце периода", typeof(string)));
            
            for (int i = 0; i < monthCalculate.Count; i++)
            {
                DataRow dr = dt.NewRow();
                    dr[0] = $"{monthCalculate[i].periodMonth.ToString()} " +
                    $" {monthCalculate[i].MonthStartingBalance.ToString()}" +
                    $" {monthCalculate[i].MonthAssessed.ToString()}" +
                    $" {monthCalculate[i].MonthPaid.ToString()}" +
                    $" {monthCalculate[i].MonthFinalBalance.ToString()}";
                dt.Rows.Add(dr);
            }

            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine(string.Join(" ", dr.ItemArray));
            }
            await File.WriteAllTextAsync(filePath, sb.ToString());
        }


        public async Task CsvForQuarter(int accId)
        {
            string filePath = @"CsvForQuarter.csv";
            DataTable dt = new DataTable();

            Calculate calculate = new Calculate(_context);


            var quarterCalculate = await calculate.Quarters(accId);

            dt.Columns.Add(new DataColumn("Квартал", typeof(string)));
            dt.Columns.Add(new DataColumn("Баланс в начале периода", typeof(string)));
            dt.Columns.Add(new DataColumn("Начислено", typeof(string)));
            dt.Columns.Add(new DataColumn("Оплачено", typeof(string)));
            dt.Columns.Add(new DataColumn("Баланс в конце периода", typeof(string)));

            for (int i = 0; i < quarterCalculate.Count; i++)
            {
                DataRow dr = dt.NewRow();
                
                    dr[0] = $"{quarterCalculate[i].periodQuarter.ToString()} " +
                    $" {quarterCalculate[i].QuarterStartingBalance.ToString()}" +
                    $" {quarterCalculate[i].QuarterAssessed.ToString()}" +
                    $" {quarterCalculate[i].QuarterPaid.ToString()}" +
                    $" {quarterCalculate[i].QarterFinalBalance.ToString()}";
                dt.Rows.Add(dr);
            }

            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendLine(string.Join(" ", dr.ItemArray));
            }
            await File.WriteAllTextAsync(filePath, sb.ToString());
        }


        public async Task CsvForYear(int accId)
        {
            string filePath = @"CsvForYear.csv";
            DataTable dt = new DataTable();

            Calculate calculate = new Calculate(_context);


            var yearCalculate = await calculate.Years(accId);

            dt.Columns.Add(new DataColumn("Год", typeof(string)));
            dt.Columns.Add(new DataColumn("Баланс в начале периода", typeof(string)));
            dt.Columns.Add(new DataColumn("Начислено", typeof(string)));
            dt.Columns.Add(new DataColumn("Оплачено", typeof(string)));
            dt.Columns.Add(new DataColumn("Баланс в конце периода", typeof(string)));

            for (int i = 0; i < yearCalculate.Count; i++)
            {
                DataRow dr = dt.NewRow();
                    dr[0] = $"{yearCalculate[i].periodYear.ToString()} " +
                    $" {yearCalculate[i].YearStartingBalance.ToString()} " +
                    $" {yearCalculate[i].YearAssessed.ToString()}" +
                    $" {yearCalculate[i].YearPaid.ToString()}" +
                    $" {yearCalculate[i].YearFinalBalance.ToString()} ";
                dt.Rows.Add(dr);
            }

            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                //sb.AppendLine(string.Join(delimeter, dr.ItemArray));
                sb.AppendLine(String.Join("", dr.ItemArray));
            }
            await File.WriteAllTextAsync(filePath, sb.ToString());
        }
    }
}
