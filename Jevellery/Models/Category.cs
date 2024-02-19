namespace Jevellery.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public List<Product> Products { get; set; }
    }
}
