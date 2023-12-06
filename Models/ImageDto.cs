namespace ShoppingCart.Models
{
    public class ImageDto
    {
        public int Id { get; set; }
        public required string Href { get; set; }
        public required string AltHref { get; set; }
        public int ProductId { get; set;}
    }
}
