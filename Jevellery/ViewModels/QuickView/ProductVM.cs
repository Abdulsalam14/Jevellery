namespace Jevellery.WebUI.ViewModels.QuickView
{
    public class ProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount {  get; set; }
        public string FileName {  get; set; }
    }
}
