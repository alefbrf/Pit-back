using CoffeeBreak.Application.Common.Enums;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAll(int page, int limit, string? name ,[FromServices] IProductService productService)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(productService.GetAll(token, page, limit, name ));
        }
        [HttpGet("count")]
        public ActionResult GetTotalCount([FromServices] IProductService productService)
        {
            return Ok(productService.GetTotalCount());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id, [FromServices] IProductService productService)
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            return Ok(productService.Get(token, id));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public ActionResult Create([FromBody] ProductDTO productDTO, [FromServices] IProductService productService)
        {
            return Ok(productService.Create(productDTO));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public ActionResult Update(int id, [FromBody] ProductDTO productDTO, [FromServices] IProductService productService)
        {
            return Ok(productService.Update(id, productDTO));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public ActionResult Delete(int id, [FromServices] IProductService productService)
        {
            productService.Delete(id);
            return Ok();
        }
    }
}
