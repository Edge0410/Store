using Store.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class OrderList : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

    }
}
