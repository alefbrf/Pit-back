using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Application.DTOs.Request.User
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
