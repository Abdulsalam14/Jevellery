
using Jevellery.ViewModels.Home;
using Jevellery.WebUI.ViewModels.Shop;
using Jewellery.Business.Abstract;
using Jewellery.Business.Concrete;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Jevellery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IFirstContentService _firstContentService;
        private readonly IWishListProductService _wishListProductService;
        private readonly IWishListService _wishListService;
        private readonly ICollectionService _collectionService;

        private UserManager<AppUser> _userManager;
        public HomeController(ICategoryService categoryService, IProductService productService = null, IFirstContentService firstContentService = null, IWishListProductService wishListProductService = null, IWishListService wishListService = null, UserManager<AppUser> userManager = null, ICollectionService collectionService = null)
        {
            _categoryService = categoryService;
            _productService = productService;
            _firstContentService = firstContentService;
            _wishListProductService = wishListProductService;
            _wishListService = wishListService;

            _userManager = userManager;
            _collectionService = collectionService;
        }


        public async Task<IActionResult> Index()
        {
            var newarrivals = await _productService.GetNewArrivalProducts();
            var featureds = await _productService.GetFeaturedProducts();

            var newarrivalProducts = new List<ProductShopVM>() { };
            var featuredProducts = new List<ProductShopVM>() { };

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                var ids = HttpContext.Request.Cookies["favoriteProducts"] != null ?
                              JsonConvert.DeserializeObject<List<int>>(HttpContext.Request.Cookies["favoriteProducts"]) :
                              new List<int>();


                foreach (var product in newarrivals)
                {

                    var productVm = new ProductShopVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Discount = product.Discount,
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Filename = product.Filename,
                        IsWishList = ids.Contains(product.Id)
                    };

                    newarrivalProducts.Add(productVm);
                }

                foreach (var product in featureds)
                {

                    var productVm = new ProductShopVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Discount = product.Discount,
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Filename = product.Filename,
                        IsWishList = ids.Contains(product.Id)
                    };

                    featuredProducts.Add(productVm);
                }
            }
            else
            {
                var wishList = await _wishListService.GetAsync(c => c.UserId == user.Id);
                if (wishList == null)
                {
                    wishList = new WishList()
                    {
                        UserId = user.Id,
                        WishListProducts = new List<WishlistProduct>()
                    };
                    await _wishListService.AddAsync(wishList);
                }
                var wishlistproducts = await _wishListProductService.GetWishListProductsBylistId(wishList.Id);
                foreach (var product in newarrivals)
                {

                    var productVm = new ProductShopVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Discount = product.Discount,
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Filename = product.Filename,
                        IsWishList = wishlistproducts.Any(wp => wp.Product.Id == product.Id)
                    };

                    newarrivalProducts.Add(productVm);
                }
                foreach (var product in featureds)
                {

                    var productVm = new ProductShopVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Discount = product.Discount,
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Filename = product.Filename,
                        IsWishList = wishlistproducts.Any(wp => wp.Product.Id == product.Id)
                    };

                    featuredProducts.Add(productVm);
                }
            }


            var model = new HomeVM
            {
                Categories = await _categoryService.GetAllAsync(),
                NewArrivalProducts = newarrivalProducts,
                FeaturedProducts = featuredProducts,
                FirstContent = await _firstContentService.GetAsync(a => a.Id > 0),
                Collections = await _collectionService.GetAllAsync()
            };
            return View(model);
        }

    }
}
