using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Application.DTOs.Request.User
{
    public class LoginCodeDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
