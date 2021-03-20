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
        /// ConnectionState özelliğini kullandık.
        /// </summary>
        /// <returns></returns>
        public IActionResult Log()
        {
            return View();
        }
    }
}
