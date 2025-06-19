using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Filters.ActionFilters;
using WebApiDemo.Filters.ExceptionFilters;
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
        [TypeFilter(typeof(ValidateShirtUpdateFilter))]
        [TypeFilter(typeof(ShirtHandleUpdateExceptionFilter))]
        public IActionResult UpdateShirt(int id, [FromBody] Shirt shirt)
        {
            var shirtToUpdate = HttpContext.Items["shirt"] as Shirt;

            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Gender = shirt.Gender;
            shirtToUpdate.Price = shirt.Price;

            _db.SaveChanges();
            return Ok(shirtToUpdate);
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(ValidateShirtIdFilter))]
        public IActionResult DeleteShirt(int id)
        {
            var shirtToDelete = HttpContext.Items["shirt"] as Shirt;

            _db.Shirts.Remove(shirtToDelete);
            _db.SaveChanges();

            return Ok(shirtToDelete);
        }
    }
}
