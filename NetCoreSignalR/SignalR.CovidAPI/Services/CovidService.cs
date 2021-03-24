using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.CovidAPI.Hubs;
using SignalR.CovidAPI.Models;
using SignalR.CovidAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.CovidAPI.Services
{
    public class CovidService : ICovidService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<CovidHub, ICovidHub> _covidHub;

        public CovidService(AppDbContext appDbContext, IHubContext<CovidHub, ICovidHub> covidHub)
        {
            _appDbContext = appDbContext;
            _covidHub = covidHub;
        }

        public async Task<List<CovidChartViewModel>> AddCovidAsync(Covid covid)
        {
            await _appDbContext.Covids.AddAsync(covid);
            await _appDbContext.SaveChangesAsync();

            var covidsChartViewModel = await GetCovidListAsync();

            //Yeni bir vaka eklendiğinde bunu tüm clientlara bildiriyorum.
            await _covidHub.Clients.All.ReceiveCovidList(covidsChartViewModel);

            return covidsChartViewModel;
        }

        public async Task<List<CovidChartViewModel>> GetCovidListAsync()
        {
            //stored procedure
            var covids = await _appDbContext.CovidPivotTableDTOs.FromSqlRaw("CovidPivotTable").ToListAsync();

            var covidsChartViewModel = new List<CovidChartViewModel>();

            foreach (var covid in covids)
            {
                var covidChartViewModel = new CovidChartViewModel
                {
                    CovidDate = covid.CovidDate
                };

                covidChartViewModel.NumberOfCases.Add(covid.Istanbul);
                covidChartViewModel.NumberOfCases.Add(covid.Ankara);
                covidChartViewModel.NumberOfCases.Add(covid.Izmir);
                covidChartViewModel.NumberOfCases.Add(covid.Konya);
                covidChartViewModel.NumberOfCases.Add(covid.Antalya);

                covidsChartViewModel.Add(covidChartViewModel);
            }

            return covidsChartViewModel;
        }
    }
}
