using Fiorello_MVC_TASK.Areas.Admin.ViewModel;
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _appDbContext.Products.FindAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var model = await _appDbContext.Products.FindAsync(id);
            if (model == null) return NotFound();

            //fileService.Delete(webHostEnvironment.WebRootPath, model.PhotoName);


            model.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var dbmodel = await _appDbContext.Products.FindAsync(id);
            if (dbmodel == null) return NotFound();

            var model = new ProductUpdateViewModel
            {
                Title = dbmodel.Title,
                Description = dbmodel.Description,
                Price = dbmodel.Price,
                PhotoName = dbmodel.PhotoName,
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var dbModel = await _appDbContext.Products.FindAsync(id);

            if (dbModel == null) return NotFound();

            dbModel.Title = model.Title;
            dbModel.Price = model.Price;
            dbModel.Description = model.Description;

            if (model.Photo != null)
            {
                if (!fileService.IsImage(model.Photo))
                {
                    ModelState.AddModelError("Photo", $"{model.Photo.FileName} named file is not image");
                    return View(model);
                }
                else
                {
                    if (!fileService.SizeCheck(model.Photo))
                    {
                        ModelState.AddModelError("Photo", $"{model.Photo.FileName} named file size must be lower than 300 kb");
                        return View(model);
                    }

                    fileService.Delete(webHostEnvironment.WebRootPath, dbModel.PhotoName);
                    dbModel.PhotoName = await fileService.UploadAsync(webHostEnvironment.WebRootPath, model.Photo);

                }
            }
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("index");
        }

    }
}
