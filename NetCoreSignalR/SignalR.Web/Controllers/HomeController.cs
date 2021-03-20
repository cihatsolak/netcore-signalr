using Microsoft.AspNetCore.Mvc;

namespace SignalR.Web.Controllers
{
    public class HomeController : Controller
    {
        //Temel Konuları Ele Aldık
        public IActionResult Basic()
        {
            return View();
        }
    }
}
