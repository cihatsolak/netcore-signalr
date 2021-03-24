using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    /// <summary>
    /// SignalR ile halısaha maçına takım kurma simülasyonu
    /// </summary>
    public class FootballHub : Hub
    {
        private readonly AppDbContext _appDbContext;
        public FootballHub(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// AddPlayerToGroupAsync: server tarafındaki metot ismidir. "Player" ise client tarafındaki metot ismidir.
        /// </summary>
        /// <returns></returns>
        public async Task AddPlayerToGroupAsync(string playerName, string teamName)
        {
            var team = _appDbContext.Teams.Include(p => p.Users)
                                          .FirstOrDefault(p => string.Equals(teamName.ToLower(), p.Name.ToLower())); //veri tabanında team var mı diye bakıyorum.
            if (team == null)
            {
                await Task.CompletedTask;
                return;
            }

            if (team.Users.Count >= 7) //takımlar en fazla 7 kişi olabilir.
            {
                //7 kişiden fazla oyuncu eklemeye çalışan olursa, ekleyen kişinin(Clients.Caller) "Warning" metotune bildirim gönder.
                await Clients.Caller.SendAsync("Warning", "the team can be a maximum of 7 people.");
                await Task.CompletedTask;
                return;
            }

            team.Users.Add(new User
            {
                Name = playerName
            });

            await _appDbContext.SaveChangesAsync(); //takıma oyuncu ekle


            /*
             * Clientlardaki metotun çalışması için istek göndereceğim. Clientlarda da bu metot tanımlıysa çalışacaktır. 
             * All: Hub'a bağlı olan tüm clientlara bildiri gönderir.
             * SendAsync("client tarafındaki metot ismi") : Clientlardaki metotu çalıştırma işlemi yapacak.
             */

            ////Player metotuna istek at, message'ına beraberinde gönder
            //await Clients.All.SendAsync("Player", playerName);

            //group'a üye olan oyunculara bildirim gönder
            await Clients.Group(teamName).SendAsync("Player", playerName, team.Id);
        }

        /// <summary>
        /// Client'ı bir takıma ekleyeceğim. Örneğin cihat isimli oyuncuyu Team A takımına ekleyeceğim. 
        /// İleride Sadece Team A oyuncularına bir bildirim atmak istediğimde bu benim işimi görecektir.
        /// </summary>
        /// <returns></returns>
        public async Task AddToGroupAsync(string teamName)
        {
            //ConnectionId: hub tarafından client'a verilen id
            string connectionId = Context.ConnectionId;

            await Groups.AddToGroupAsync(connectionId, teamName);
        }

        /// <summary>
        /// Client'ı bir takımdan çıkmak isteyebilir. Örneğin cihat isimli oyuncuyu Team A takımına eklemiştik. 
        /// Cihat a takımından çıkarsa, A takımına atılan bildirimlerden haberi olmaz
        /// </summary>
        /// <returns></returns>
        public async Task RemoveFromGroupAsync(string teamName)
        {
            //ConnectionId: hub tarafından client'a verilen id
            string connectionId = Context.ConnectionId;

            await Groups.RemoveFromGroupAsync(connectionId, teamName);
        }
    }
}
