using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models
{
    public class Shirt
    {
        public int Id { get; set; }

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public string Size { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        public double? Price { get; set; }
    }
}
