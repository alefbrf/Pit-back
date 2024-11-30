using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Application.DTOs.Request.Admin
{
    public class ConfigDTO
    {
        [MaxLength(400)]
        public string Address { get; set; }
        public decimal DeliveryTax { get; set; }
    }
}
