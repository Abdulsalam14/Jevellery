using Jewellery.Entities.Models;
using Jewellery.Entities;
using Microsoft.AspNetCore.Mvc;
using Jewellery.Business.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CategoryController(ICategoryService categoryService, IWebHostEnvironment hostingEnvironment)
        {
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Category category, IFormFile categoryImage)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var isexists = await _categoryService.GetAsync(c => c.Name.ToLower() == category.Name.ToLower()); /*_dbContext.Categories.AnyAsync(c => c.Title.ToLower().Trim() == category.Title.ToLower().Trim());*/
            if (isexists != null)
            {
                ModelState.AddModelError("Name", "Kateqoriya movcuddur");
                return View(category);
            }
            if (categoryImage == null || categoryImage.Length == 0)
            {
                return View(category);
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
            category.Filename = categoryImage.FileName;
            await _categoryService.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCategory = await _categoryService.GetAsync(c => c.Id == id);
            await _categoryService.DeleteAsync(deletedCategory);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updatedCategory = await _categoryService.GetAsync(c => c.Id == id);
            return View(updatedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category, IFormFile categoryImage)
        {
            if (categoryImage == null || categoryImage.Length == 0)
            {
                ModelState.Remove("categoryImage");
            }
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var isexists = await _categoryService.GetAsync(c => c.Name.ToLower().Trim() == category.Name.ToLower().Trim() && c.Id != category.Id);
            if (isexists != null)
            {
                ModelState.AddModelError("Name", "Kateqoriya movcuddur");
                return View(category);
            }

            if (categoryImage != null && categoryImage.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                string filePath = Path.Combine(uploadsFolder, categoryImage.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await categoryImage.CopyToAsync(fileStream);
                }
                category.Filename = categoryImage.FileName;
            }


            await _categoryService.UpdateAsync(category);
            return RedirectToAction(nameof(Index));

        }
    }
}
