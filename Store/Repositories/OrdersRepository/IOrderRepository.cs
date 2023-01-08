using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.OrdersRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        OrderWithProductsDto ShowDetails(Guid id);
    }
}
