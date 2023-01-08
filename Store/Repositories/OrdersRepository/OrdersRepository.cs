using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.OrdersRepository
{
    public class OrdersRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrdersRepository(AppDbContext context) : base(context)
        {

        }

        public OrderWithProductsDto ShowDetails(Guid id)
        {
            var orderwithproducts = _context.Orders.Where(n => n.Id == id).Select(order => new OrderWithProductsDto() {
                DeliveryAddress = order.DeliveryAddress,
                Description = order.Description,
                Username = order.User.Username,
                ProductNames = order.OrderList.Select(n => n.Product.Name).ToList()
            }).FirstOrDefault();

            return orderwithproducts;
        }

    }
}
