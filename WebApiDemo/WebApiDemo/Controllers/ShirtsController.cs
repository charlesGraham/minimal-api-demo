using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Filters;
using WebApiDemo.Models;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public List<Shirt> GetShirts()
        {
            return ShirtRepo.GetShirts();
        }

        [HttpGet("{id}")]
        [ValidateShirtIdFilter]
        public IActionResult GetShirt(int id)
        {
            return Ok(ShirtRepo.GetShirt(id));
        }

        [HttpPost]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            if (shirt == null)
                return BadRequest("Failed to create shirt. Shirt details are missing.");

            var createdShirt = ShirtRepo.AddShirt(shirt);

            if (createdShirt == null)
                return BadRequest("Failed to create shirt.");

            return CreatedAtAction(nameof(GetShirt), new { id = createdShirt.Id }, createdShirt);
        }

        [HttpPut("{id}")]
        [ValidateShirtIdFilter]
        public IActionResult UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            var updatedShirt = ShirtRepo.UpdateShirt(shirt);

            if (updatedShirt == null)
                return BadRequest("Failed to update shirt.");

            return Ok(updatedShirt);
        }

        [HttpDelete("{id}")]
        [ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
            var result = ShirtRepo.DeleteShirt(id);
            return NoContent();
        }
    }
}
