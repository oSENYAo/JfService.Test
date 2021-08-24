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

        [HttpGet("{id}")]
        public async Task<IActionResult> Year(int accId = 808251)   // need change
        {
            var years = await _calculate.Years(accId);

            return View(years);
        }

        [HttpGet]
        public async Task<IActionResult> Quarter(int accId = 808251)
        {
            var quarters = await _calculate.Quarters(accId);

            return View(quarters);
        }

        [HttpGet]
        public async Task<IActionResult> Month(int accId = 808251)
        {
            var months = await _calculate.Monts(accId);

            return View(months);
        }
    }
}
