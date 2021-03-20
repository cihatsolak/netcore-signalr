using Microsoft.AspNetCore.SignalR;
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
        public static List<string> Names { get; set; } = new List<string>();

        /// <summary>
        /// SendNameAsync: server tarafındaki metot ismidir. "ReceiveName" ise client tarafındaki metot ismidir.
        /// </summary>
        /// <returns></returns>
        public async Task SendNameAsync(string message)
        {
            /*
             * Clientlardaki metotun çalışması için istek göndereceğim. Clientlarda da bu metot tanımlıysa çalışacaktır. 
             * All: Hub'a bağlı olan tüm clientlara bildiri gönderir.
             * SendAsync("client tarafındaki metot ismi") : Clientlardaki metotu çalıştırma işlemi yapacak.
             */

            //ReceiveMessage metotuna istek at, message'ına beraberinde gönder
            await Clients.All.SendAsync("ReceiveName", message);
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
    }
}
