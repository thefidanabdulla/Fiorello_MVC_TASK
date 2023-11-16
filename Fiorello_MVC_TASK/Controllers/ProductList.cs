using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Controllers
{
    public class ProductList : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
