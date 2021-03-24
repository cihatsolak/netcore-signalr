using Microsoft.AspNetCore.Mvc;

namespace SignalR.Web.Controllers
{
    //SignalR.CovidAPI servisindeki metotlara bağlanacak.
    public class CovidController : Controller
    {
        public IActionResult Covid19Show()
        {
            return View();
        }
    }
}
