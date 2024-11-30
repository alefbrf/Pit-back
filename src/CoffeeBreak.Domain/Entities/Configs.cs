using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Domain.Entities
{
    public class Configs
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(400)]
        public string Address { get; set; }
        public decimal DeliveryTax { get; set; }
    }
}
