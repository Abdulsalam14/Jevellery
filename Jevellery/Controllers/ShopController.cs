using Microsoft.AspNetCore.Mvc;

namespace Jevellery.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
