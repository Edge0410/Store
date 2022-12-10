using System.ComponentModel.DataAnnotations;

namespace Store.Models.DTOs
{
    public class ProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
