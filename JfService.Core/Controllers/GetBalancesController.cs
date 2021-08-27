using JfService.Core.ViewModels;
using JFService.Data;
using JFService.Service;
using JFService.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JfService.Core.Controllers
{
    public class GetBalancesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly ICalculate<YearService, MonthService, QuarterService> _calculate;

        public GetBalancesController(DataManager dataManager,
            ICalculate<YearService, MonthService, QuarterService> calculate)
        {
            _dataManager = dataManager;
            _calculate = calculate;
        }

        public async Task<IActionResult> Index(int accId = 808251)
        {
            IndexViewModel model = new IndexViewModel();
            model.Years = await _calculate.Years(accId);
            model.Quarters = await _calculate.Quarters(accId);
            model.Months = await _calculate.Monts(accId);
            return View(model);
        }
        public IActionResult ResulTest()
        {
            return View();
        }

    }
}
