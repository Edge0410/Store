using Store.Models.Base;

namespace Store.Models
{
    public class Order : BaseEntity
    {
        public string DeliveryAddress { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public List<OrderList> OrderList { get; set; }

    }
}
