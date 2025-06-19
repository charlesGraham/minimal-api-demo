using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Filters;
using WebApiDemo.Filters.ActionFilters;
using WebApiDemo.Models;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private readonly ShirtRepo _shirtRepo;
        private readonly ApplicationDbContext _db;

        public ShirtsController(ShirtRepo shirtRepo, ApplicationDbContext db)
        {
            _shirtRepo = shirtRepo;
            _db = db;
        }

        [HttpGet]
        public List<Shirt> GetShirts()
        {
            return _db.Shirts.ToList();
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ValidateShirtIdFilter))]
        public IActionResult GetShirt(int id)
        {
            return Ok(HttpContext.Items["shirt"]);
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateShirtCreateFilter))]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            _db.Shirts.Add(shirt);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetShirt), new { id = shirt.Id }, shirt);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(ValidateShirtIdFilter))]
        public IActionResult UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            var updatedShirt = _shirtRepo.UpdateShirt(id, shirt);

            if (updatedShirt == null)
                return BadRequest("Failed to update shirt.");

            return Ok(updatedShirt);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(ValidateShirtIdFilter))]
        public IActionResult DeleteShirt(int id)
        {
            var result = _shirtRepo.DeleteShirt(id);

            if (!result)
                return BadRequest("Failed to delete shirt.");

            return NoContent();
        }
    }
}
