using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [MaxLength(300)]
        public string? ImageUrl { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<ProductCart> ProductsCart { get; set; } = new List<ProductCart>();
    }
}
