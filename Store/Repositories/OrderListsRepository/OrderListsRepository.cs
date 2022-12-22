using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.OrderListsRepository
{
    public class OrderListsRepository : GenericRepository<OrderList>, IOrderListRepository
    {
        public OrderListsRepository(AppDbContext context) : base(context)
        {
            
        }

        public OrderList FindByIds(Guid order, Guid product)
        {
            return _table.FirstOrDefault(x => x.OrderId == order && x.ProductId == product);
        }
    }
}
