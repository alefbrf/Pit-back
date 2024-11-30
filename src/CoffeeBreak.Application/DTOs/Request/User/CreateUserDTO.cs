using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Application.DTOs.Request.User
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha obrigatório")]
        [MinLength(8, ErrorMessage = "Senha pequena")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
