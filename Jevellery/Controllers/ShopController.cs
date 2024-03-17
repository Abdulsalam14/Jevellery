
using Jevellery.ViewModels.Shop;
using Jevellery.WebUI.ViewModels.Shop;
using Jewellery.Business.Abstract;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;

namespace Jevellery.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWishListProductService _wishListProductService;
        private readonly IWishListService _wishListService;

        private UserManager<AppUser> _userManager;


        public ShopController(IProductService productService, ICategoryService categoryService, UserManager<AppUser> userManager, IWishListProductService wishListProductService, IWishListService wishListService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _wishListProductService = wishListProductService;
            _wishListService = wishListService;
        }

        public async Task<IActionResult> Index(string sort = "default", int page = 1, int categoryId = 0, int filterMin = 0, int filterMax = 0)
        {
            List<Product> products;
            if (categoryId > 0)
            {
                products = await _productService.GetProductsByCategory(categoryId);
            }
            else
            {
                products = await _productService.GetAllAsync();
            }


            var minPrice = products.Min(p =>
            {
                if (p.Discount > 0)
                    return p.Price - (p.Price * ((decimal)p.Discount / 100));
                else
                    return p.Price;
            });

            var maxPrice = products.Max(p =>
            {
                if (p.Discount > 0)
                    return p.Price - (p.Price * ((decimal)p.Discount / 100));
                else
                    return p.Price;
            });
            if (filterMin > 0)
            {
                products = products.Where(p =>
                {
                    if (p.Discount > 0)
                        return (p.Price - (p.Price * ((decimal)p.Discount / 100))) > filterMin;
                    else
                        return p.Price > filterMin;
                }).ToList();
            }
            if (filterMax > 0) products = products.Where(p =>
            {
                if (p.Discount > 0)
                    return (p.Price - (p.Price * ((decimal)p.Discount / 100))) < filterMax;
                else
                    return p.Price < filterMax;
            }).ToList();
            if (sort == "latest")
            {
                products = products.OrderByDescending(p => p.Id).ToList();
            }
            else if (sort == "high")
            {
                products = products.OrderByDescending(p =>
                {
                    decimal discountedPrice = p.Discount > 0 ? (p.Price - (p.Price * ((decimal)p.Discount / 100))) : p.Price;
                    return discountedPrice;
                }).ToList();

            }
            else if (sort == "low")
            {
                products = products.OrderBy(p =>
                {
                    decimal discountedPrice = p.Discount > 0 ? (p.Price - (p.Price * ((decimal)p.Discount / 100))) : p.Price;
                    return discountedPrice;
                }).ToList();
            }

            var productsvm = new List<ProductShopVM>()
            {

            };

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                var ids = HttpContext.Request.Cookies["favoriteProducts"] != null ?
                              JsonConvert.DeserializeObject<List<int>>(HttpContext.Request.Cookies["favoriteProducts"]) :
                              new List<int>();


                foreach (var product in products)
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

                    productsvm.Add(productVm);
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

                foreach (var product in products)
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

                    productsvm.Add(productVm);
                }
            }


            int pageSize = 4;
            var PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize));
            productsvm = productsvm.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var CurrentPage = page;



            var model = new ShopVM
            {
                Products = productsvm,
                Selects = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "default",
                        Text = "Default sorting",
                        Selected = sort == "default"

                    },

                    new SelectListItem
                    {
                        Value = "latest",
                        Text = "Sort by latest",
                        Selected = sort == "latest"
                    },
                    new SelectListItem
                    {
                        Value = "low",
                        Text = "Sort by price: low to high",
                        Selected = sort == "low"

                    },
                    new SelectListItem
                    {
                        Value = "high",
                        Text = "Sort by price: high to low",
                        Selected = sort == "high"

                    }
                },
                CurrentPage = CurrentPage,
                PageCount = PageCount,
                Sort = sort,
                CategoryId = categoryId,
                Categories = await _categoryService.GetAllAsync(),
                FilterMax = filterMax,
                FilterMin = filterMin,
                MaxProductPrice = (int)maxPrice,
                MinProductPrice = (int)minPrice

            };
            return View(model);
        }
    }
}
