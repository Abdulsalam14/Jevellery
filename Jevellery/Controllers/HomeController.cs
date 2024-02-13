using Jevellery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jevellery.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
