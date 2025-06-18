using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetShirt(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var foundShirt = ShirtRepo.GetShirt(id);

            if (foundShirt == null)
                return NotFound("Shirt not found.");

            return Ok(foundShirt);
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
        public IActionResult UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            if (shirt == null)
                return BadRequest("Failed to update shirt. Shirt details are missing.");

            var updatedShirt = ShirtRepo.UpdateShirt(shirt);

            if (updatedShirt == null)
                return BadRequest("Failed to update shirt.");

            return Ok(updatedShirt);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var deletedShirt = ShirtRepo.DeleteShirt(id);

            if (deletedShirt == null)
                return NotFound("Shirt not found.");

            return Ok(deletedShirt);
        }
    }
}
