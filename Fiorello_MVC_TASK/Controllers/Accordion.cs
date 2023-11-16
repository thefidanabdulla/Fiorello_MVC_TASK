using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Controllers
{
    public class Accordion : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
