using Store.Models;
using Store.Repositories.OrdersRepository;

namespace Store.Services.Orders
{
    public class OrdersService : IOrderService
    {
        public IOrderRepository _orderRepository;

        public OrdersService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Create(Order newOrder)
        {
            await _orderRepository.CreateAsync(newOrder);
            await _orderRepository.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var order = _orderRepository.FindById(id);
            _orderRepository.Delete(order);
            await _orderRepository.SaveAsync();
        }

    }
}
