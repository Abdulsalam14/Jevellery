using Jevellery.ViewModels.Cart;
using Jevellery.WebUI.ViewModels.QuickView;
using Jevellery.WebUI.ViewModels.WishList;
using Jewellery.Business.Abstract;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Text.Json;

namespace Jevellery.WebUI.Controllers
{
    public class WishListController : Controller
    {

        private UserManager<AppUser> _userManager;
        private readonly IWishListProductService _wishlistProductService;
        private readonly IWishListService _wishListService;
        private readonly IProductService _productService;

        public WishListController(UserManager<AppUser> userManager, IWishListProductService wishListProductService, IWishListService wishListService, IProductService productService)
        {
            _userManager = userManager;
            _wishlistProductService = wishListProductService;
            _wishListService = wishListService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                List<int> favoriteProducts = HttpContext.Request.Cookies["favoriteProducts"] != null ?
                          JsonConvert.DeserializeObject<List<int>>(HttpContext.Request.Cookies["favoriteProducts"]) :
                          new List<int>();

                var products = await _productService.GetWishListProducts(favoriteProducts);
                var vm = new WishListVM
                {
                    Products = products,
                    Count = 0
                };
                return View(vm);

            }
            var wishList = await _wishListService.GetAsync(c => c.UserId == user.Id);

            var wishListProducts = await _wishlistProductService.GetWishListProductsBylistId(wishList.Id);
            var pr = wishListProducts.Select(p => p.Product).ToList();
            var vmm = new WishListVM
            {
                
                Products = pr,
                Count = 0
            };
            return View(vmm);
        }


        public async Task<IActionResult> Add(int productId)
        {
            var product = await _productService.GetAsync(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {

                List<int> favoriteProducts = HttpContext.Request.Cookies["favoriteProducts"] != null ?
                                   JsonConvert.DeserializeObject<List<int>>(HttpContext.Request.Cookies["favoriteProducts"]) :
                                   new List<int>();

                // Favori ürünler listesine yeni ürünü ekleyin
                if (!favoriteProducts.Contains(productId))
                {
                    favoriteProducts.Add(productId);

                    HttpContext.Response.Cookies.Append("favoriteProducts", JsonConvert.SerializeObject(favoriteProducts),
                     new CookieOptions
                     {
                         Expires = DateTime.Now.AddHours(2)
                     });
                }

            }
            else
            {
                WishList wishList = await _wishListService.GetAsync(c => c.UserId == user.Id);
                if (wishList == null)
                {
                    wishList = new WishList()
                    {
                        UserId = user.Id,
                    };
                    await _wishListService.AddAsync(wishList);
                }
                WishlistProduct wishListProduct = await _wishlistProductService.GetAsync(cp => cp.ProductId == productId && cp.WishlistId == wishList.Id);
                if (wishListProduct == null)
                {
                    wishListProduct = new WishlistProduct
                    {
                        ProductId = productId,
                        WishlistId = wishList.Id,
                    };
                    await _wishlistProductService.AddAsync(wishListProduct);
                }
            }
            return Ok();
        }




        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                List<int> favoriteProducts = HttpContext.Request.Cookies["favoriteProducts"] != null ?
                                    JsonConvert.DeserializeObject<List<int>>(HttpContext.Request.Cookies["favoriteProducts"]) :
                                    new List<int>();

                if (favoriteProducts.Contains(id))
                {

                    favoriteProducts.Remove(id);
                    HttpContext.Response.Cookies.Append("favoriteProducts", JsonConvert.SerializeObject(favoriteProducts),
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddHours(2)
                        });
                }


            }
            else
            {
                var wishlistProduct = await _wishlistProductService.GetAsync(cp => cp.ProductId == id);
                if (wishlistProduct == null) return NotFound();
                await _wishlistProductService.DeleteAsync(wishlistProduct);
            }
            return Ok();
        }


        public async Task<IActionResult> GetWishListProducts()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                List<int> favoriteProducts = HttpContext.Request.Cookies["favoriteProducts"] != null ?
                          JsonConvert.DeserializeObject<List<int>>(HttpContext.Request.Cookies["favoriteProducts"]) :
                          new List<int>();

                var products = await _productService.GetWishListProducts(favoriteProducts);
                var vm = new WishListVM
                {
                    Products = products,
                    Count = 0
                };
                return PartialView("_WishListContentPartialView", vm);

            }
            var wishList = await _wishListService.GetAsync(c => c.UserId == user.Id);

            var wishListProducts = await _wishlistProductService.GetWishListProductsBylistId(wishList.Id);
            var pr = wishListProducts.Select(p => p.Product).ToList();
            var vmm = new WishListVM
            {

                Products = pr,
                Count = 0
            };
            return PartialView("_WishListContentPartialView", vmm);
        }
    }
}
