using Store.Models;

namespace Store.Services.Orders
{
    public interface IOrderService
    {
        Task Create(Order newOrder);
    }
}
