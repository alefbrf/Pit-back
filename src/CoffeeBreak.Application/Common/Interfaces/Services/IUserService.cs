using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.DTOs.Request.User;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task Register(CreateUserDTO userDTO, Role role);
        Task<User> ValidateAccount(string email, string sCode);
        Task<string> Login(string email, string password);
        Task<string> LoginCode(string email, string sCode);
        Task SendCode(string email);
        Task ChangePassword(string jwtToken, string sCode, string NewPassword);
        List<User> GetDeliverieMans();
    }
}
