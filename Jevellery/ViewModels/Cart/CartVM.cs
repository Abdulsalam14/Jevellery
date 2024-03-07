using Jewellery.Entities.Models;

namespace Jevellery.ViewModels.Cart
{
    public class CartVM
    {
        public List<CartProduct> CartProducts { get; set; }
        public decimal Sum { get; set; }
        public int Count { get; set; }
    }
}
