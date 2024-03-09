
using Jevellery.ViewModels.Home;
using Jewellery.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jevellery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(ICategoryService categoryService, IProductService productService = null)
        {
            _categoryService = categoryService;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            var model = new HomeVM
            {
                Categories = await _categoryService.GetAllAsync(),
                NewArrivalProducts = await _productService.GetNewArrivalProducts(),
                FeaturedProducts=await _productService.GetFeaturedProducts(),
            };
            return View(model);
        }

    }
}
