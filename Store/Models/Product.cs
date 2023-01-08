using Store.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public List<OrderList> OrderList { get; set; }
    }
}
