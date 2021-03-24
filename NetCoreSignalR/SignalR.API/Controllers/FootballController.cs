using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SignalR.API.Hubs;
using SignalR.API.Models;
using SignalR.API.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace SignalR.API.Controllers
{
    /// <summary>
    /// Maksimum 7 kişilik olacak bir halı saha ekibi ayarlamaya çalışacağız ve kadro dolduğunda takıma başka kişi eklenemeyecek.
    /// </summary>
    [ApiController, Route("api/[controller]/[action]")]
    public class FootballController : ControllerBase
    {
        private readonly IHubContext<FootballHub> _footballHubContext;
        private readonly AppDbContext _appDbContext;

        public FootballController(
            IHubContext<FootballHub> footballHubContext,
            AppDbContext appDbContext)
        {
            _footballHubContext = footballHubContext;
            _appDbContext = appDbContext;
        }

        [HttpGet("{teamCount}")]
        public async Task<IActionResult> SetTeamCount(int teamCount)
        {
            string message = string.Concat("Arkadaşlar takım", teamCount, "kişi olacaktır."); //Client tarafına göndereceğim bildirim mesajı

            await _footballHubContext.Clients.All.SendAsync("Notify", message);  //Tüm clientlara bildirim gönderiyorum.

            return Ok();
        }

        /// <summary>
        /// Client sayfayı il açtığında bu servise istek atıp takımlardaki oyuncuları listeleyecek.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPlayersInTeam()
        {
            var teams = await _appDbContext.Teams.Include(p => p.Users).ToListAsync();
            if (!teams.Any())
                return NotFound();

            var playerTeamViewModels = new List<PlayerTeamViewModel>();

            teams.ForEach((team) =>
            {
                var playerTeamViewModel = new PlayerTeamViewModel()
                {
                    TeamName = team.Name,
                    Users = team.Users.Select(p => p.Name)
                };

                playerTeamViewModels.Add(playerTeamViewModel);
            });

            return Ok(playerTeamViewModels);
        }
    }
}
