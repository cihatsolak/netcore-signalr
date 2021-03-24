using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.CovidAPI.Hubs;
using SignalR.CovidAPI.Models;
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

        public async Task<List<CovidPivotTableDTO>> AddCovidAsync(Covid covid)
        {
            await _appDbContext.Covids.AddAsync(covid);
            await _appDbContext.SaveChangesAsync();

            var covids = await GetCovidPivotTableAsync();

            //Yeni bir vaka eklendiğinde bunu tüm clientlara bildiriyorum.
            await _covidHub.Clients.All.ReceiveCovidList(covids);

            return covids;
        }

        public async Task<List<CovidPivotTableDTO>> GetCovidPivotTableAsync()
        {
            return await _appDbContext.CovidPivotTableDTOs.FromSqlRaw("CovidPivotTable").ToListAsync();
        }
    }
}
