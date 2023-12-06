using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public required string Preview { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
