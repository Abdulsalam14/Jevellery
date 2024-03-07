using Jewellery.Entities.Models;

namespace Jevellery.ViewModels.Home
{
    public class HomeVM
    {
        public List<Category>? Categories { get; set; }
        public List<Product> NewArrivalProducts { get; set; }
    }
}
