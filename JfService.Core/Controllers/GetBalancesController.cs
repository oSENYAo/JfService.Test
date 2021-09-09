using JfService.Core.ViewModels;
using JFService.Data;
using JFService.Service;
using JFService.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace JfService.Core.Controllers
{
    public class GetBalancesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly ICalculate<YearService, MonthService, QuarterService> _calculate;
        private readonly Xml _xml;
        private readonly Csv _csv;
        private readonly IWebHostEnvironment _webHost;

        public GetBalancesController(DataManager dataManager,
            ICalculate<YearService, MonthService, QuarterService> calculate,
            Xml xml,
            Csv csv,
            IWebHostEnvironment webHost)
        {
            _dataManager = dataManager;
            _calculate = calculate;
            _xml = xml;
            _csv = csv;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index(int accId = 808251)
        {
            await _dataManager.BalanceRepository.UpdateDatabase();
            IndexViewModel model = new IndexViewModel();
            model.Years = await _calculate.Years(accId);
            model.Quarters = await _calculate.Quarters(accId);
            model.Months = await _calculate.Monts(accId);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> XmlForMonths(int accId = 808251)
        {
            await _xml.ParseXmlForMonth(accId);
            // Путь к файлу
            string file_path = Path.Combine(_webHost.ContentRootPath, "InfoForMonths.xml");
            // Тип файла - content-type
            string file_type = "application/xml";
            // Имя файла - необязательно
            string file_name = "InfoForMonths.xml";
            return PhysicalFile(file_path, file_type, file_name);
        }
        [HttpPost]
        public async Task<ActionResult> XmlForQuarter(int accId = 808251)
        {
            await _xml.ParseXmlForQuarter(accId);
            // Путь к файлу
            string file_path = Path.Combine(_webHost.ContentRootPath, "InfoForQuarters.xml");
            // Тип файла - content-type
            string file_type = "application/xml";
            // Имя файла - необязательно
            string file_name = "InfoForQuarters.xml";
            return PhysicalFile(file_path, file_type, file_name);
        }
        [HttpPost]
        public async Task<ActionResult> XmlForYear(int accId = 808251)
        {
            await _xml.ParseXmlForYear(accId);
            // Путь к файлу
            string file_path = Path.Combine(_webHost.ContentRootPath, "InfoForYears.xml");
            // Тип файла - content-type
            string file_type = "application/xml";
            // Имя файла - необязательно
            string file_name = "InfoForYears.xml";
            return PhysicalFile(file_path, file_type, file_name);
        }

        [HttpPost]
        public async Task<ActionResult> CsvForMonths(int accId = 808251)
        {
            await _csv.CsvForMonth(accId);
            // Путь к файлу
            string file_path = Path.Combine(_webHost.ContentRootPath, "CsvForMonth.csv");
            // Тип файла - content-type
            string file_type = "text/csv";
            // Имя файла - необязательно
            string file_name = "CsvForMonth.csv";
            return PhysicalFile(file_path, file_type, file_name);
        }

        [HttpPost]
        public async Task<ActionResult> CsvForQuarter(int accId = 808251)
        {
            await _csv.CsvForQuarter(accId);
            // Путь к файлу
            string file_path = Path.Combine(_webHost.ContentRootPath, "CsvForQuarter.csv");
            // Тип файла - content-type
            string file_type = "text/csv";
            // Имя файла - необязательно
            string file_name = "CsvForQuarter.csv";
            return PhysicalFile(file_path, file_type, file_name);
        }
        [HttpPost]
        public async Task<ActionResult> CsvForYear(int accId = 808251)
        {
            await _csv.CsvForYear(accId);
            // Путь к файлу
            string file_path = Path.Combine(_webHost.ContentRootPath, "CsvForYear.csv");
            // Тип файла - content-type
            string file_type = "text/csv";
            // Имя файла - необязательно
            string file_name = "CsvForYear.csv";
            return PhysicalFile(file_path, file_type, file_name);
        }

    }
}
