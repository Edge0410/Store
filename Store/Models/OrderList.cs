using Store.Models.Base;

namespace Store.Models
{
    public class OrderList : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}
