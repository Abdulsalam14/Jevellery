using Jewellery.Business.Abstract;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile categoryImage)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var isexists = await _productService.GetAsync(c => c.Name.ToLower() == product.Name.ToLower()); /*_dbContext.Categories.AnyAsync(c => c.Title.ToLower().Trim() == category.Title.ToLower().Trim());*/
            if (isexists != null)
            {
                ModelState.AddModelError("Name", "Kateqoriya movcuddur");
                return View(product);
            }
            if (categoryImage == null || categoryImage.Length == 0)
            {
                return View(product);
            }
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
            string filePath = Path.Combine(uploadsFolder, categoryImage.FileName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await categoryImage.CopyToAsync(fileStream);
            }
            product.Filename = categoryImage.FileName;
            await _productService.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCategory = await _productService.GetAsync(c => c.Id == id);
            await _productService.DeleteAsync(deletedCategory);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updatedCategory = await _productService.GetAsync(c => c.Id == id);
            return View(updatedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile categoryImage)
        {
            if (categoryImage == null || categoryImage.Length == 0)
            {
                ModelState.Remove("categoryImage");
            }
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var isexists = await _productService.GetAsync(c => c.Name.ToLower().Trim() == product.Name.ToLower().Trim() && c.Id != product.Id);
            if (isexists != null)
            {
                ModelState.AddModelError("Name", "Kateqoriya movcuddur");
                return View(product);
            }

            if (categoryImage != null && categoryImage.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                string filePath = Path.Combine(uploadsFolder, categoryImage.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await categoryImage.CopyToAsync(fileStream);
                }
                product.Filename = categoryImage.FileName;
            }


            await _productService.UpdateAsync(product);
            return RedirectToAction(nameof(Index));

        }
    }
}
