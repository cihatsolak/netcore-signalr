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
        public static List<string> Participants { get; set; } = new List<string>(); //halı sahaya katılacaklar.

        /// <summary>
        /// AddPlayersMatchAsync: server tarafındaki metot ismidir. "Player" ise client tarafındaki metot ismidir.
        /// </summary>
        /// <returns></returns>
        public async Task AddPlayersMatchAsync(string playerName)
        {
            if (Participants.Count >= 7) //Katılımcı sayısı 7 kişiden fazla ise
            {
                //7 kişiden fazla oyuncu eklemeye çalışan olursa, ekleyen kişinin(Clients.Caller) "Warning" metotune bildirim gönder.
                await Clients.Caller.SendAsync("Warning", "the team can be a maximum of 7 people.");
            }
            else
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
}
