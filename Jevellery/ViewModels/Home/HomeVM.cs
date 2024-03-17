using Jevellery.WebUI.ViewModels.Shop;
using Jewellery.Entities.Models;

namespace Jevellery.ViewModels.Home
{
    public class HomeVM
    {
        public List<Category>? Categories { get; set; }
        public List<ProductShopVM>? NewArrivalProducts { get; set; }
        public List<ProductShopVM>? FeaturedProducts { get; set; }

        public List<Collection>? Collections { get; set; }
        public HomeFirstContent? FirstContent { get; set; }
    }
}
