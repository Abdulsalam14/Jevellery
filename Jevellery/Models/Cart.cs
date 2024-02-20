namespace Jevellery.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartProduct>? CartProducts { get; set; }
    }
}
