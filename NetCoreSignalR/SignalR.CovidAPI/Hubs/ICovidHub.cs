using SignalR.CovidAPI.Models;
using SignalR.CovidAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.CovidAPI.Hubs
{
    /// <summary>
    /// Client tarafından abone olunacak metot isimlerini burada tanımlıyoruz.
    /// </summary>
    public interface ICovidHub
    {
        Task ReceiveCovidList(List<CovidChartViewModel> covidChartViewModels);
    }
}
