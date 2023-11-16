using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
