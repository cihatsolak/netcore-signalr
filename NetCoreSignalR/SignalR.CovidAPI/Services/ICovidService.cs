using SignalR.CovidAPI.Models;
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
        Task<List<CovidPivotTableDTO>> AddCovidAsync(Covid covid);

        /// <summary>
        /// Stored Procedure - Covid vakalarının illere göre pivot table haline getirilmiş
        /// </summary>
        /// <returns></returns>
        Task<List<CovidPivotTableDTO>> GetCovidPivotTableAsync();
    }
}
