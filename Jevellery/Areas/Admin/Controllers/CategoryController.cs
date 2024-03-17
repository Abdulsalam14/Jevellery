using Jewellery.Entities.Models;
using Jewellery.Entities;
using Microsoft.AspNetCore.Mvc;
using Jewellery.Business.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;
        public CategoryController(ICategoryService categoryService,  IPhotoService photoService)
        {
            _categoryService = categoryService;
            _photoService = photoService;
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
                ModelState.AddModelError("categoryImage", "photo is required");
                return View(category);
            }


            category.Filename = await _photoService.UploadImageAsync(categoryImage);
            await _categoryService.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCategory = await _categoryService.GetAsync(c => c.Id == id);

            var publicid = _photoService.GetPublicIdFromUrl(deletedCategory.Filename);
            await _photoService.DeleteImageAsync(publicid);
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
                var publicid = _photoService.GetPublicIdFromUrl(category.Filename);
                category.Filename=await _photoService.UpdateImageAsync(categoryImage, publicid);
            }


            await _categoryService.UpdateAsync(category);
            return RedirectToAction(nameof(Index));

        }
    }
}
