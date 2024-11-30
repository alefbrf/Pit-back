using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CoffeeBreak.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private IAddressService _addressService;
        public AddressController([FromServices] IAddressService addressService) 
        {
            _addressService = addressService;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            var user = Context.Context.GetUser(HttpContext);
            return Ok(_addressService.GetAll(user.Id));
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var user = Context.Context.GetUser(HttpContext);
            return Ok(_addressService.GetById(user.Id, id));
        }

        [HttpPost]
        public ActionResult Create([FromBody] AddressDTO addressDTO)
        {
            var user = Context.Context.GetUser(HttpContext);
            return Ok(_addressService.Create(user.Id, addressDTO));
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] AddressDTO addressDTO)
        {
            return Ok(_addressService.Update(id, addressDTO));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _addressService.Delete(id);
            return Ok();
        }
    }
}
