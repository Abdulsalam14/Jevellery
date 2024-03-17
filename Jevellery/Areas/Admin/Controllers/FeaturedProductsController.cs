using Jevellery.WebUI.Areas.Admin.Models;
using Jewellery.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FeaturedProductsController : Controller
    {
        private readonly IProductService _productService;

        public FeaturedProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetFeaturedProducts();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new NewArrivalsUpdateVM
            {
                OtherProducts = await _productService.GetNotFeaturedProducts(),
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(NewArrivalsUpdateVM createvm)
        {
            var selected = await _productService.GetAsync(p => p.Id == createvm.SelectedId);
            selected.IsFeatured = true;
            await _productService.UpdateAsync(selected);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.GetAsync(c => c.Id == id);
            deletedProduct.IsFeatured = false;
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
                OtherProducts = await _productService.GetNotFeaturedProducts(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewArrivalsUpdateVM updateVM)
        {
            var selected = await _productService.GetAsync(p => p.Id == updateVM.SelectedId);
            var updated = await _productService.GetAsync(p => p.Id == updateVM.Current.Id);
            selected.IsFeatured = true;
            updated.IsFeatured = false;
            await _productService.UpdateAsync(selected);
            await _productService.UpdateAsync(updated);
            return RedirectToAction(nameof(Index));
        }
    }
}
