using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Href { get; set; }

        [Required]
        public required string AltHref { get; set; }
    }
}
