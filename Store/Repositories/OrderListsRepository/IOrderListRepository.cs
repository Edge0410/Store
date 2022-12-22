using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.OrderListsRepository
{
    public interface IOrderListRepository : IGenericRepository<OrderList>
    {
        OrderList FindByIds(Guid order, Guid product);
    }
}
