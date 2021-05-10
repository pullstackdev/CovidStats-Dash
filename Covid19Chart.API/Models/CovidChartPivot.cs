using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Chart.API.Models
{
    public class CovidChartPivot
    {
        public CovidChartPivot()
        {
            Counts = new List<int>();
        }
        public string CovidDate { get; set; }
        public List<int> Counts { get; set; } //property sayısı değişirse (şuan 5) diye list tutuldu
    }
}
