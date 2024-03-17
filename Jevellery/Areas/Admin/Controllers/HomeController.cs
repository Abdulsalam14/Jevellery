using Jevellery.WebUI.Areas.Admin.Models;
using Jewellery.Business.Abstract;
using Jewellery.Business.Concrete;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IFirstContentService _firstContentService;
        private readonly IPhotoService _photoService;

        public HomeController(IFirstContentService firstContentService, IPhotoService photoService)
        {
            _firstContentService = firstContentService;
            _photoService = photoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FirstContent()
        {
            var vm = await _firstContentService.GetAsync(a => a.Id > 0);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditFirstContent()
        {
            var content = await _firstContentService.GetAsync(a => a.Id > 0);

            return View(content);
        }

        [HttpPost]
        public async Task<IActionResult> EditFirstContent(HomeFirstContent content, IFormFile? newImage)
        {
            var oldcontent = await _firstContentService.GetAsync(p => p.Id >0 );
            if (newImage == null || newImage.Length == 0)
            {
                ModelState.Remove("newImage");
            }
            if (newImage != null && newImage.Length > 0)
            {
                var publicid = _photoService.GetPublicIdFromUrl(oldcontent.ImageUrl);

                content.ImageUrl = await _photoService.UpdateImageAsync(newImage, publicid);
            }
            oldcontent.Title = content.Title;
            oldcontent.Description=content.Description;
            oldcontent.ImageUrl= content.ImageUrl;

            await _firstContentService.UpdateAsync(oldcontent);
            return RedirectToAction(nameof(Index));

        }
    }
}
