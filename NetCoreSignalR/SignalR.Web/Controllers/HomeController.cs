using Microsoft.AspNetCore.Mvc;

namespace SignalR.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Temel konuları ele aldık. Hub'a baglantı kurduk ve mesaj gönderip mesaj aldık.
        /// </summary>
        /// <returns></returns>
        public IActionResult Basic()
        {
            return View();
        }

        /// <summary>
        /// SignalR'da loglama seviyesine göre loglama yapısı oluşturduk.
        /// Bağlantı durumlarına göre aksiyon aldık. Bağlantının kopması vs gibi durumlarda.
        /// </summary>
        /// <returns></returns>
        public IActionResult ConnectionState()
        {
            return View();
        }

        /// <summary>
        /// OnConnectedAsync(), OnDisconnectedAsync() metotlarının kullanımı
        /// </summary>
        /// <returns></returns>
        public IActionResult HubVirtualMethods()
        {
            return View();
        }

        /// <summary>
        /// IHubContext<THub> kullanımı
        /// </summary>
        /// <returns></returns>
        public IActionResult FootballHub()
        {
            return View();
        }
    }
}
