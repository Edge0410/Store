using System.ComponentModel.DataAnnotations;

namespace Store.Models.DTOs
{
    public class ProductDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int QuantitySold { get; set; }
    }
}
