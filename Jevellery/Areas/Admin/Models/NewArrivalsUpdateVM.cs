using Jewellery.Entities.Models;

namespace Jevellery.WebUI.Areas.Admin.Models
{
    public class NewArrivalsUpdateVM
    {
        public Product?  Current { get; set; }
        public List<Product>? OtherProducts { get; set; }

        public int SelectedId { get; set; }
    }
}
