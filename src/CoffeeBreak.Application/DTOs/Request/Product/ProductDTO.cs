using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Application.DTOs.Request.Product
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]

        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"^(\d{0,5})(\.\d{0,2})?$")]
        [Range(0.00, 99999.99, ErrorMessage = "Somente números positivos.")]
        public decimal Price { get; set; }

        [MaxLength(300)]
        public string? ImageUrl { get; set; }
    }
}
