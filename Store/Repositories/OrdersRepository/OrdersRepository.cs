using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.OrdersRepository
{
    public class OrdersRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrdersRepository(AppDbContext context) : base(context)
        {

        }

    }
}
