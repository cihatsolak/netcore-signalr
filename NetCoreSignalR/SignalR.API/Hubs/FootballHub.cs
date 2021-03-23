using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    /// <summary>
    /// SignalR ile halısaha maçına takım kurma simülasyonu
    /// </summary>
    public class FootballHub : Hub
    {
        // <summary>
        /// Eğer property'i static olarak tanımlamazsak, client'dan gelecek her istekde MyHub class'ı new'leneceği için
        /// liste sürekli new() edilecek ve veriyi kaybedeceğiz. O nedenle property'i static olarak tanımlamak önemli.
        /// </summary>
        private static List<string> Participants { get; set; } = new List<string>(); //halı sahaya katılacaklar.

        /// <summary>
        /// SendNameAsync: server tarafındaki metot ismidir. "Player" ise client tarafındaki metot ismidir.
        /// </summary>
        /// <returns></returns>
        public async Task AddPlayersMatch(string playerName)
        {
            Participants.Add(playerName); //Listeye oyuncuyu ekliyorum.

            /*
             * Clientlardaki metotun çalışması için istek göndereceğim. Clientlarda da bu metot tanımlıysa çalışacaktır. 
             * All: Hub'a bağlı olan tüm clientlara bildiri gönderir.
             * SendAsync("client tarafındaki metot ismi") : Clientlardaki metotu çalıştırma işlemi yapacak.
             */

            //Player metotuna istek at, message'ına beraberinde gönder
            await Clients.All.SendAsync("Player", playerName);
        }
    }
}
