namespace ShoppingCart.Models
{
    public class ProductForCreationDto
    {
        public required string Name { get; set; }
        public int Price { get; set; }
        public required string Preview { get; set; }
    }
}
