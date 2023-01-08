using Store.Models;
using Store.Models.DTOs;

namespace Store.Services.Orders
{
    public interface IOrderService
    {
        Task Create(Order newOrder);
        Order FindById(Guid id);
        OrderWithProductsDto ShowDetails(Guid id);
        Task Edit(Guid id, OrderRequestDto editOrder);
        Task Delete(Guid id);
    }
}
