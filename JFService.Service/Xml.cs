using JFService.Data.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JFService.Service
{
    public class Xml
    {
        private readonly AppDbContext _context;

        public Xml(AppDbContext context)
        {
            _context = context;
        }
        public async Task ParseXmlForMonth(int accId)
        {
            Calculate calculate = new Calculate(_context);

            XDocument xDocument = new XDocument();

            var monthCalculate = await calculate.Monts(accId);
            
            List<XElement> xes = new List<XElement>();

            foreach (var _balance in monthCalculate)
            {
                XElement period = new XElement("period", _balance.periodMonth);
                XElement in_balance = new XElement("in_balance", _balance.MonthStartingBalance);
                XElement calculation = new XElement("calculation", _balance.MonthAssessed);
                XElement sum = new XElement("sum", _balance.MonthPaid);
                XElement finalBalance = new XElement("finalBalance", _balance.MonthFinalBalance);

                // новые точки XML(balance)

                XElement balance = new XElement("balance");

                balance.Add(period);
                balance.Add(in_balance);
                balance.Add(calculation);
                balance.Add(sum);
                balance.Add(finalBalance);

                xes.Add(balance);

            }

            XElement account = new XElement("account");
            foreach (var Xbalance in xes)
            {
                account.Add(Xbalance);
            }
            xDocument.Add(account);
            xDocument.Save("InfoForMonths.xml");
        }
        public async Task ParseXmlForQuarter(int accId)
        {
            Calculate calculate = new Calculate(_context);

            XDocument xDocument = new XDocument();

            var quarterCalculate = await calculate.Quarters(accId);

            List<XElement> xes = new List<XElement>();

            foreach (var _balance in quarterCalculate)
            {
                XElement period = new XElement("period", _balance.periodQuarter);
                XElement in_balance = new XElement("in_balance", _balance.QuarterStartingBalance);
                XElement calculation = new XElement("calculation", _balance.QuarterAssessed);
                XElement sum = new XElement("sum", _balance.QuarterPaid);
                XElement finalBalance = new XElement("finalBalance", _balance.QarterFinalBalance);

                // новые точки XML(balance)

                XElement balance = new XElement("balance");

                balance.Add(period);
                balance.Add(in_balance);
                balance.Add(calculation);
                balance.Add(sum);
                balance.Add(finalBalance);

                xes.Add(balance);

            }

            XElement account = new XElement("account");
            foreach (var Xbalance in xes)
            {
                account.Add(Xbalance);
            }
            xDocument.Add(account);
            xDocument.Save("InfoForQuarters.xml");
        }
        public async Task ParseXmlForYear(int accId)
        {
            Calculate calculate = new Calculate(_context);

            XDocument xDocument = new XDocument();

            var yearCalculate = await calculate.Years(accId);

            List<XElement> xes = new List<XElement>();

            foreach (var _balance in yearCalculate)
            {
                XElement period = new XElement("period", _balance.periodYear);
                XElement in_balance = new XElement("in_balance", _balance.YearStartingBalance);
                XElement calculation = new XElement("calculation", _balance.YearAssessed);
                XElement sum = new XElement("sum", _balance.YearPaid);
                XElement finalBalance = new XElement("finalBalance", _balance.YearFinalBalance);

                // новые точки XML(balance)

                XElement balance = new XElement("balance");

                balance.Add(period);
                balance.Add(in_balance);
                balance.Add(calculation);
                balance.Add(sum);
                balance.Add(finalBalance);

                xes.Add(balance);

            }

            XElement account = new XElement("account");
            foreach (var Xbalance in xes)
            {
                account.Add(Xbalance);
            }
            xDocument.Add(account);
            xDocument.Save("InfoForYears.xml");
        }
    }
}
