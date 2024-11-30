using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("my")]
        public IActionResult GetMyOrders()
        {
            var user = Context.Context.GetUser(HttpContext);
            return Ok(_orderService.GetAll(user.Id));
        }

        [HttpGet("deliveries")]
        [Authorize(Roles = $"{nameof(Role.Admin)}, {nameof(Role.DeliveryMan)}")]
        public IActionResult GetDeliveries()
        {
            var user = Context.Context.GetUser(HttpContext);
            if (user.Role == (byte)Role.DeliveryMan)
            {
                return Ok(_orderService.GetDeliveries(user.Id));
            }

            return Ok(_orderService.GetDeliveries(null));
        }

        [HttpGet("pending")]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult GetPending()
        {
            return Ok(_orderService.GetPending());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_orderService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDTO orderDTO)
        {
            var user = Context.Context.GetUser(HttpContext);
            return Ok(_orderService.CreateOrder(orderDTO, user.Id));
        }

        [HttpPost("approve")]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult Approve([FromBody] int id)
        {
            return Ok(_orderService.Approve(id));
        }

        [HttpPost("disapprove")]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult Disapprove([FromBody] int id)
        {
            return Ok(_orderService.Disapprove(id));
        }
        [HttpPost("make-ready")]
        [Authorize(Roles = nameof(Role.Admin))]
        public IActionResult MakeReady([FromBody] int id)
        {
            return Ok(_orderService.MakeReady(id));
        }
        [HttpPost("sent")]
        [Authorize(Roles = $"{nameof(Role.Admin)}, {nameof(Role.DeliveryMan)}")]
        public IActionResult Sent([FromBody] int id)
        {
            return Ok(_orderService.Sent(id));
        }
        [HttpPost("deliver")]
        [Authorize(Roles = $"{nameof(Role.Admin)}, {nameof(Role.DeliveryMan)}")]
        public IActionResult Deliver([FromBody] int id)
        {
            return Ok(_orderService.Deliver(id));
        }
    }
}
