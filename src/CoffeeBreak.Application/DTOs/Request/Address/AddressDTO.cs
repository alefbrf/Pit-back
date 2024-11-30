using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Application.DTOs.Request.Address
{
    public class AddressDTO
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de 100 caracteres")]
        public string Neighborhood { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo de 200 caracteres")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(10, ErrorMessage = "Tamanho máximo de 10 caracteres")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(10, ErrorMessage = "Tamanho máximo de 10 caracteres")]
        public string Number { get; set; }
        [MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
        public string Complement { get; set; }
    }
}
