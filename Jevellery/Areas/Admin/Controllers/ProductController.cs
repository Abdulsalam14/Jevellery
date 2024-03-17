using Jevellery.WebUI.Areas.Admin.Models;
using Jevellery.WebUI.ViewModels.QuickView;
using Jewellery.Business.Abstract;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jevellery.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;

        public ProductController(IProductService productService,  ICategoryService categoryService, IPhotoService photoService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            var viewModel = new ProductCreateVM
            {
                Product = new(),
                Categories = categories
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productvm, IFormFile productImage)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        var errorMessage = error.ErrorMessage;
                    }
                }
                productvm.Categories = await _categoryService.GetAllAsync();
                return View(productvm);
            }
            var isexists = await _productService.GetAsync(c => c.Name.ToLower() == productvm.Product.Name.ToLower());
            if (isexists != null)
            {
                ModelState.AddModelError("Name", "Product movcuddur");
                productvm.Categories = await _categoryService.GetAllAsync();
                return View(productvm);
            }
            if (productImage == null || productImage.Length == 0)
            {
                productvm.Categories = await _categoryService.GetAllAsync();
                return View(productvm);
            }

            productvm.Product.Filename = await _photoService.UploadImageAsync(productImage);
            await _productService.AddAsync(productvm.Product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.GetAsync(c => c.Id == id);
            var publicid = _photoService.GetPublicIdFromUrl(deletedProduct.Filename);
            await _photoService.DeleteImageAsync(publicid);
            await _productService.DeleteAsync(deletedProduct);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updatedProduct = await _productService.GetAsync(c => c.Id == id);
            var model = new ProductCreateVM { Product = updatedProduct, Categories = await _categoryService.GetAllAsync() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateVM productVm, IFormFile? productImage)
        {
            var product = await _productService.GetAsync(p => p.Id == productVm.Product.Id);
            if (productImage == null || productImage.Length == 0)
            {
                ModelState.Remove("productImage");
                productVm.Product.Filename = product.Filename;
            }
            if (!ModelState.IsValid)
            {
                productVm.Categories = await _categoryService.GetAllAsync();

                return View(productVm);
            }

            if (productImage != null && productImage.Length > 0)
            {

                var publicid =  _photoService.GetPublicIdFromUrl(product.Filename);

                productVm.Product.Filename = await _photoService.UpdateImageAsync(productImage,publicid);
            }

            product.Filename = productVm.Product.Filename;
            product.Price = productVm.Product.Price;
            product.IsFeatured = productVm.Product.IsFeatured;
            product.IsNewArrival=productVm.Product.IsNewArrival;
            product.CategoryId= productVm.Product.CategoryId;
            product.Description = productVm.Product.Description;
            product.Discount = productVm.Product.Discount;
            product.Name= productVm.Product.Name;


            await _productService.UpdateAsync(product);
            return RedirectToAction(nameof(Index));

        }
    }
}
