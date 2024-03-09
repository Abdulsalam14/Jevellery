using Jevellery.WebUI.Areas.Admin.Models;
using Jewellery.Business.Abstract;
using Jewellery.Business.Concrete;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewArrivalsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;

        public NewArrivalsController(IProductService productService, IPhotoService photoService)
        {
            _productService = productService;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var model=await _productService.GetNewArrivalProducts();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new NewArrivalsUpdateVM
            {
                OtherProducts = await _productService.GetNotNewArrivalProducts(),
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(NewArrivalsUpdateVM createvm)
        {
            var selected = await _productService.GetAsync(p => p.Id == createvm.SelectedId);
            selected.IsNewArrival = true;
            await _productService.UpdateAsync(selected);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.GetAsync(c => c.Id == id);
            deletedProduct.IsNewArrival = false;
            await _productService.UpdateAsync(deletedProduct);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updatedProduct = await _productService.GetAsync(c => c.Id == id);
            var model = new NewArrivalsUpdateVM
            {
                Current = updatedProduct,
                OtherProducts = await _productService.GetNotNewArrivalProducts(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewArrivalsUpdateVM updateVM )
        {
            var selected = await _productService.GetAsync(p => p.Id == updateVM.SelectedId);
            var updated = await _productService.GetAsync(p => p.Id == updateVM.Current.Id);
            selected.IsNewArrival = true;
            updated.IsNewArrival = false;
            await _productService.UpdateAsync(selected);
            await _productService.UpdateAsync(updated);
            return RedirectToAction(nameof(Index));
        }
    }
}
