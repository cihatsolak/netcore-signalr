using Microsoft.AspNetCore.Mvc;

namespace SignalR.Web.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
