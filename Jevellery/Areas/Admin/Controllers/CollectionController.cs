using Jewellery.Business.Abstract;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly IPhotoService _photoService;
        public CollectionController(ICollectionService collectionService, IPhotoService photoService)
        {
            _collectionService = collectionService;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var collections = await _collectionService.GetAllAsync();
            return View(collections);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Collection collection, IFormFile collectionImage)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            var isexists = await _collectionService.GetAsync(c => c.Title.ToLower() == collection.Title.ToLower()); /*_dbContext.Categories.AnyAsync(c => c.Title.ToLower().Trim() == category.Title.ToLower().Trim());*/
            if (isexists != null)
            {
                ModelState.AddModelError("Title", "Title movcuddur");
                return View(collection);
            }
            if (collectionImage == null || collectionImage.Length == 0)
            {
                ModelState.AddModelError("collectionImage", "photo is required");
                return View(collection);
            }


            collection.ImageUrl = await _photoService.UploadImageAsync(collectionImage);
            await _collectionService.AddAsync(collection);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedCollection = await _collectionService.GetAsync(c => c.Id == id);

            var publicid = _photoService.GetPublicIdFromUrl(deletedCollection.ImageUrl);
            await _photoService.DeleteImageAsync(publicid);
            await _collectionService.DeleteAsync(deletedCollection);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updatedCollection = await _collectionService.GetAsync(c => c.Id == id);
            return View(updatedCollection);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Collection collection, IFormFile collectionImage)
        {
            if (collectionImage == null || collectionImage.Length == 0)
            {
                ModelState.Remove("collectionImage");
            }
            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            var isexists = await _collectionService.GetAsync(c => c.Title.ToLower().Trim() == collection.Title.ToLower().Trim() && c.Id != collection.Id);
            if (isexists != null)
            {
                ModelState.AddModelError("Name", "Kateqoriya movcuddur");
                return View(collection);
            }

            if (collectionImage != null && collectionImage.Length > 0)
            {
                var publicid = _photoService.GetPublicIdFromUrl(collection.ImageUrl);
                collection.ImageUrl = await _photoService.UpdateImageAsync(collectionImage, publicid);
            }


            await _collectionService.UpdateAsync(collection);
            return RedirectToAction(nameof(Index));

        }

    }
}
