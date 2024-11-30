using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAll(int page, int limit, string? name, [FromServices] IFavoriteService favoriteService)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(favoriteService.GetAll(token, page, limit, name));
        }

        [HttpGet("count")]
        public ActionResult GetTotalCount([FromServices] IFavoriteService favoriteService)
        {
            string? token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(favoriteService.GetTotaCount(token));
        }

        [HttpPost]
        public ActionResult Create([FromBody] int productId, [FromServices] IFavoriteService favoriteService)
        {
            string? token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(favoriteService.Create(token, productId));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromServices] IFavoriteService favoriteService)
        {
            string? token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            favoriteService.Delete(token,id);
            return Ok();
        }
    }
}
