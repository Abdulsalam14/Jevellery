namespace Jevellery.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Filename {  get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int  CategoryId { get; set; }
        public Category Category { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }
}
