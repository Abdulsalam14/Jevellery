using Jevellery.Models;
using Jevellery.Services.Abstract;
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


        public IActionResult Index()
        {
            return View();
        }

    }
}
