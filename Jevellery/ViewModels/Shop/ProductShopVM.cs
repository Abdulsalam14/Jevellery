using Jewellery.Entities.Models;

namespace Jevellery.WebUI.ViewModels.Shop
{
    public class ProductShopVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Filename { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsWishList {  get; set; }
    }
}
