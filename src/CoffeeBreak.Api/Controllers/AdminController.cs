using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Admin;
using CoffeeBreak.Application.DTOs.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IConfigsService _configsService;
        public AdminController([FromServices] IUserService userService, IOrderService orderService, IConfigsService configsService)
        {
            _userService = userService;
            _orderService = orderService;
            _configsService = configsService;
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("delivery-man")]
        public IActionResult GetDeliverieMans()
        {
            return Ok(_userService.GetDeliverieMans());
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPost("update-delivery-man/{id}")]
        public IActionResult UpdateOrderDeliveryMan(int id, [FromBody] int? deliveryManId)
        {
            return Ok(_orderService.UpdateDeliveryMan(id, deliveryManId));
        }

        [HttpGet("config")]
        public IActionResult GetConfig()
        {
            return Ok(_configsService.GetConfigs());
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPost("config")]
        public IActionResult SaveConfigs([FromBody] ConfigDTO config)
        {
            return Ok(_configsService.Update(config.Address, config.DeliveryTax));
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPost("create-delivery-man")]
        public async Task<IActionResult> CreateDeliveryMan([FromBody] CreateUserDTO userDTO)
        {
            await _userService.Register(userDTO, Role.DeliveryMan);
            return Ok("Um e-mail foi enviado com o código de confirmação.");
        }
    }
}
