using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jevellery.WebUI.Areas.Admin.Models
{
    public class ProductCreateVM
    {
        public Product?  Product { get; set; }

        public List <Category>? Categories { get; set; }
    }
}
