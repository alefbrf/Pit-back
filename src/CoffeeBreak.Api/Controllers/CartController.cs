using CoffeeBreak.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAll(int page, int limit, string? name, [FromServices] IProductCartService productCartService)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(productCartService.GetAll(token, page, limit, name));
        }

        [HttpGet("count")]
        public ActionResult GetTotalCount([FromServices] IProductCartService productCartService)
        {
            string? token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(productCartService.GetTotaCount(token));
        }

        [HttpPost]
        public ActionResult Create([FromBody] int id, [FromServices] IProductCartService productCartService)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(productCartService.Create(token, id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromServices] IProductCartService productCartService)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            productCartService.Delete(token, id);
            return Ok();
        }
    }
}
