using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class ProductForCreationDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public required string Name { get; set; }
        public int Price { get; set; }
        public required string Preview { get; set; }
    }
}
