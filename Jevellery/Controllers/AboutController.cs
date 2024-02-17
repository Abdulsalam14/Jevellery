using Microsoft.AspNetCore.Mvc;

namespace Jevellery.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
