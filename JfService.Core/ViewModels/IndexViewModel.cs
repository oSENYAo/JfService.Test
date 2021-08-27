using JFService.Service;
using System.Collections.Generic;

namespace JfService.Core.ViewModels
{
    public class IndexViewModel
    {
        public List<YearService> Years { get; set; } = new List<YearService>();
        public List<QuarterService> Quarters { get; set; } = new List<QuarterService>();
        public List<MonthService> Months { get; set; } = new List<MonthService>();
    }
}
