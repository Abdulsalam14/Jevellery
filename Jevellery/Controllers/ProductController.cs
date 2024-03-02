using Jevellery.Models;
using Jevellery.Services.Abstract;
using Jevellery.ViewModels.Productt;
using Microsoft.AspNetCore.Mvc;

namespace Jevellery.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int productId)
        {
            var product = await _productService.Get(p => p.Id == productId);
            var relatedProducts = await _productService.GetProductsByCategory(product.Category.Id);
            var model = new ProductIndexVM
            {
                Product = product,
                RelatedProducts = relatedProducts.Where(p => p.Id != productId).Take(4).ToList()
            };


            return View(model);
        }
    }
}
