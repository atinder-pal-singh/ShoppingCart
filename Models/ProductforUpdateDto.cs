namespace ShoppingCart.Models
{
    public class ProductforUpdateDto
    {
        public required string Name { get; set; }
        public int Price { get; set; }
        public required string Preview { get; set; }
    }
}
