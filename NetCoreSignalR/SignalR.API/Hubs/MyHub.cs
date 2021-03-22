using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    /// <summary>
    /// SignalR Hub
    /// Projede Hub class'ını kullanmak için ekstra bir kütüphane eklememe gerek yok.
    /// Bunun içerisinde clientların çağıracağı metotları asenkron şekilde tanımlayacağım
    /// Clientlar hub'a her istek attığında MyHub classından sıfırdan bir örnek oluşur.
    /// </summary>
    public class MyHub : Hub
    {
        /// <summary>
        /// Eğer property'i static olarak tanımlamazsak, client'dan gelecek her istekde MyHub class'ı new'leneceği için
        /// liste sürekli new() edilecek ve veriyi kaybedeceğiz. O nedenle property'i static olarak tanımlamak önemli.
        /// </summary>
        private static List<string> Names { get; set; } = new List<string>();

        /// <summary>
        /// Client sayısını tutacağım property
        /// </summary>
        private static int ClientCount { get; set; } = 0;

        /// <summary>
        /// SendNameAsync: server tarafındaki metot ismidir. "ReceiveName" ise client tarafındaki metot ismidir.
        /// </summary>
        /// <returns></returns>
        public async Task SendNameAsync(string name)
        {
            /*
             * Clientlardaki metotun çalışması için istek göndereceğim. Clientlarda da bu metot tanımlıysa çalışacaktır. 
             * All: Hub'a bağlı olan tüm clientlara bildiri gönderir.
             * SendAsync("client tarafındaki metot ismi") : Clientlardaki metotu çalıştırma işlemi yapacak.
             */

            //ReceiveMessage metotuna istek at, message'ına beraberinde gönder
            await Clients.All.SendAsync("ReceiveName", name);
        }

        /// <summary>
        /// Client benim GetNamesAsync metotumu dediklediğinde bende client tarafındaki "ReceiveNames" metotunu tetikliyorum.
        /// </summary>
        /// <returns></returns>
        public async Task GetNamesAsync()
        {
            /*
             * Tüm Clientları bilgilendir.
             * Clientlar üzerindeki "ReceiveNames" metotu çalışsın.
             */
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        /// <summary>
        /// Clientlar hub'a bağlandıkça bu metot otomatik olarak çalışır
        /// </summary>
        /// <returns></returns>
        public async override Task OnConnectedAsync()
        {
            ClientCount++; //Bağlantı kapatıldığı için client sayısınından 1 arttırıyorum

            //Client tarafında "RecevieClientCount" metotuna abone olmuş herkese gider
            await Clients.All.SendAsync("RecevieClientCount", ClientCount);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            ClientCount--; //Bağlantı kapatıldığı için client sayısınından 1 düşürüyorum

            //Client tarafında "RecevieClientCount" metotuna abone olmuş herkese gider
            await Clients.All.SendAsync("RecevieClientCount", ClientCount);
        }
    }
}
