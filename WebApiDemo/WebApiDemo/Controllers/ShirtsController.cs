using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private static List<Shirt> shirts = new List<Shirt>
        {
            new Shirt
            {
                Id = 1,
                Color = "Red",
                Size = 10,
                Brand = "Nike",
                Gender = "Men",
                Price = 30.00,
            },
            new Shirt
            {
                Id = 2,
                Color = "Blue",
                Size = 12,
                Brand = "Adidas",
                Gender = "Women",
                Price = 25.00,
            },
            new Shirt
            {
                Id = 3,
                Color = "Green",
                Size = 14,
                Brand = "Puma",
                Gender = "Men",
                Price = 35.00,
            },
        };

        [HttpGet]
        public List<Shirt> GetShirts()
        {
            return shirts;
        }

        [HttpGet("{id}")]
        public IActionResult GetShirt(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID");

            var foundShirt = shirts.FirstOrDefault(s => s.Id == id);

            if (foundShirt == null)
            {
                return NotFound("Shirt not found.");
            }

            return Ok(foundShirt);
        }

        [HttpPost]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            shirts.Add(shirt);
            return CreatedAtAction(nameof(GetShirt), new { id = shirt.Id }, shirt);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            var foundShirt = shirts.FirstOrDefault(s => s.Id == id);

            if (foundShirt == null)
                return NotFound("Shirt not found.");

            foundShirt.Color = shirt.Color;
            foundShirt.Size = shirt.Size;
            foundShirt.Brand = shirt.Brand;
            foundShirt.Gender = shirt.Gender;
            foundShirt.Price = shirt.Price;

            return Ok(foundShirt);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            var foundShirt = shirts.FirstOrDefault(s => s.Id == id);

            if (foundShirt == null)
                return NotFound("Shirt not found.");

            shirts.Remove(foundShirt);
            return Ok(foundShirt);
        }
    }
}
