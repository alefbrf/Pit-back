using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBreak.Application.DTOs.Request.User
{
    public class ChangePasswordDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Por favor, insira senhas iguais")]
        public string ConfirmationPassword { get; set; }
    }
}
