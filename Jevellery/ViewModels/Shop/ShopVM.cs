using Jevellery.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jevellery.ViewModels.Shop
{
    public class ShopVM
    {
        public List<Product> Products { get; set; }
        public List<SelectListItem> Selects { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public string Sort { get; set; }
    }
}
