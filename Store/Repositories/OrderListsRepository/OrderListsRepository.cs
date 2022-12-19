using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.OrderListsRepository
{
    public class OrderListsRepository : GenericRepository<OrderList>, IOrderListRepository
    {
        public OrderListsRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
