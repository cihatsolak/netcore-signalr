using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.API.Hubs;
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
        public FootballController(IHubContext<FootballHub> footballHubContext)
        {
            _footballHubContext = footballHubContext;
        }

        [HttpGet("{teamCount}")]
        public async Task<IActionResult> SetTeamCount(int teamCount)
        {
            string message = string.Concat("Arkadaşlar takım", teamCount, "kişi olacaktır."); //Client tarafına göndereceğim bildirim mesajı

            await _footballHubContext.Clients.All.SendAsync("Notify", message);  //Tüm clientlara bildirim gönderiyorum.

            return Ok();
        }

        [HttpGet]
        public IActionResult GetParticipants()
        {
            return Ok(FootballHub.Participants);
        }
    }
}
