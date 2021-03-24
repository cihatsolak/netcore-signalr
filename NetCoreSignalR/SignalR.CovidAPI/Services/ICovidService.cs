using SignalR.CovidAPI.Models;
using SignalR.CovidAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.CovidAPI.Services
{
    public interface ICovidService
    {
        /// <summary>
        /// Veri tabanına covid vakalarının dummy data olarak eklenmesi
        /// </summary>
        /// <param name="covid"></param>
        /// <returns></returns>
        Task<List<CovidChartViewModel>> AddCovidAsync(Covid covid);

        /// <summary>
        /// Stored Procedure - Covid vakalarının illere göre pivot table'dan viewmodel'e dönüştürülmüş hali
        /// </summary>
        /// <returns></returns>
        Task<List<CovidChartViewModel>> GetCovidListAsync();
    }
}
