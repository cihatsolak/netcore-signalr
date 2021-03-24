using System.Collections.Generic;

namespace SignalR.CovidAPI.Models.ViewModels
{
    /// <summary>
    /// Grafik için oluşturulan view model
    /// </summary>
    public class CovidChartViewModel
    {
        public CovidChartViewModel()
        {
            NumberOfCases = new List<int>();
        }

        public string CovidDate { get; set; }
        public List<int> NumberOfCases { get; set; }
    }
}
