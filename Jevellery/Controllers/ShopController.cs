using Jevellery.Models;
using Jevellery.Services.Abstract;
using Jevellery.ViewModels.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jevellery.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
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
                products = products.OrderByDescending(p => p.Price).ToList();

            }
            else if (sort == "low")
            {
                products = products.OrderBy(p => p.Price).ToList();
            }
            int pageSize = 4;
            var PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize));
            products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var CurrentPage = page;
            var model = new ShopVM
            {
                Products = products,
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
