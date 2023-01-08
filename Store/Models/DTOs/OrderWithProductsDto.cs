using Store.Models.Base;

namespace Store.Models.DTOs
{
    public class OrderWithProductsDto
    {
        public string DeliveryAddress { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public List<string> ProductNames { get; set; }
    }
}
