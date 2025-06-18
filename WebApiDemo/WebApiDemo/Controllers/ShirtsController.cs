using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public string GetShirts()
        {
            return "Reading all the shirts";
        }

        [HttpGet("{id}")]
        public string GetShirt(int id)
        {
            return $"Reading the shirt with id: {id}";
        }

        [HttpPost]
        public string CreateShirt([FromBody] Shirt shirt)
        {
            return $"Creating a new shirt: {shirt.Color} {shirt.Size} {shirt.Brand} {shirt.Gender} {shirt.Price}";
        }

        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating the shirt with id: {id}";
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Deleting the shirt with id: {id}";
        }
    }
}
