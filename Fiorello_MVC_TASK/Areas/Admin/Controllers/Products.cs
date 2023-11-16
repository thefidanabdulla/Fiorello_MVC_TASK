using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Products : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
