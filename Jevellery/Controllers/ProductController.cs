
using Jevellery.ViewModels.Productt;
using Jevellery.WebUI.ViewModels.QuickView;
using Jewellery.Business.Abstract;
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
            var product = await _productService.GetAsync(p => p.Id == productId);
            var relatedProducts = await _productService.GetProductsByCategory(product.Category.Id);
            var model = new ProductIndexVM
            {
                Product = product,
                RelatedProducts = relatedProducts.Where(p => p.Id != productId).Take(4).ToList()
            };


            return View(model);
        }

        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _productService.GetAsync(p => p.Id == productId);
            var model = new ProductVM
            {
                Name=product.Name,
                CategoryName=product.Category.Name,
                CategoryId=product.Category.Id,
                Price=product.Price,
                ProductId=productId,
                Description=product.Description,
                FileName=product.Filename
            };

            return PartialView("_QuickViewPartialView", model);
        }


    }
}
