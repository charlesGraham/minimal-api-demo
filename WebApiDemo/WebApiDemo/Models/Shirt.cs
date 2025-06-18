using System.ComponentModel.DataAnnotations;
using WebApiDemo.Models.Validations;

namespace WebApiDemo.Models
{
    public class Shirt
    {
        public int Id { get; set; }

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        [ShirtSizeValidator]
        public int Size { get; set; }

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        public double? Price { get; set; }
    }
}
