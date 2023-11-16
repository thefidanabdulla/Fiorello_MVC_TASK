using Fiorello_MVC_TASK.DAL;
using Fiorello_MVC_TASK.Helpers;
using Fiorello_MVC_TASK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC_TASK.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFileService fileService;

        public ProductsController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _appDbContext = appDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _appDbContext.Products.ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {


            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (!fileService.IsImage(product.Photo))
            {
                ModelState.AddModelError("Photo", "File must be image format");
                return View(product);
            }

            if (!fileService.SizeCheck(product.Photo))
            {
                ModelState.AddModelError("Photo", "File size is greater than 100 kb");
                return View(product);
            }



            product.PhotoName = await fileService.UploadAsync(webHostEnvironment.WebRootPath, product.Photo);
            await _appDbContext.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
