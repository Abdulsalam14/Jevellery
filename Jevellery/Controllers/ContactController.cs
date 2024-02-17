using Microsoft.AspNetCore.Mvc;

namespace Jevellery.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
