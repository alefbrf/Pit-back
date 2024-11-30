using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AuthController : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CreateUserDTO userDTO, [FromServices] IUserService service)
        {
            await service.Register(userDTO, Role.Client);
            return Ok("Você recebera um e-mail com um código para confirmar sua conta.");
        }

        [HttpPost("validate-account")]
        public async Task<ActionResult> ValidateAccount([FromBody] ValidadeAccountDTO validadeAccountDTO, [FromServices] IUserService service)
        {
            var user = await service.ValidateAccount(validadeAccountDTO.Email, validadeAccountDTO.Code);
            return Ok($"Tudo certo \"{user.Name}\", sua conta foi validada com sucesso.");
        }

        [HttpPost("login")]  
        public async Task<ActionResult> Login([FromBody] LoginDTO login, [FromServices] IUserService service)
        {
            var token = await service.Login(login.Email, login.Password);
            return Ok(token);
        }

        [HttpPost("login-code")]
        public async Task<ActionResult> LoginCode([FromBody] LoginCodeDTO login, [FromServices] IUserService service)
        {
            var token = await service.LoginCode(login.Email, login.Code);
            return Ok(token);
        }

        [HttpPost("request-code")]
        public async Task<ActionResult> RequestCode([FromBody] string email, [FromServices] IUserService service)
        {
            await service.SendCode(email);
            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO, [FromServices] IUserService service)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            await service.ChangePassword(token, changePasswordDTO.Code, changePasswordDTO.NewPassword);
            return Ok();
        }
    }
}
