using Microsoft.AspNetCore.SignalR;
using SignalR.CovidAPI.Services;
using System.Threading.Tasks;

namespace SignalR.CovidAPI.Hubs
{
    /// <summary>
    /// Client tarafından invoke edilecek metotlar
    /// </summary>
    public class CovidHub : Hub<ICovidHub>
    {
        private readonly ICovidService _covidService;
        public CovidHub(ICovidService covidService)
        {
            _covidService = covidService;
        }

        public async Task GetCovidListAsync()
        {
            var covids = await _covidService.GetCovidPivotTableAsync();

            //ReceiveCovidList metot ismi ICovidHub'dan gelmektedir.
            await Clients.All.ReceiveCovidList(covids);
        }
    }
}
