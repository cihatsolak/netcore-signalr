using Microsoft.AspNetCore.Mvc;

namespace SignalR.Web.Controllers
{
    public class StreamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
