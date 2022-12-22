using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.OrderListsRepository;

namespace Store.Services.OrderLists
{
    public class OrderListsService : IOrderListService
    {
        public IOrderListRepository _orderlistRepository;
        
        public OrderListsService(IOrderListRepository orderlistRepository)
        {
            _orderlistRepository = orderlistRepository;
        }

        public async Task Create(OrderListRequestDto newList)
        {
            await _orderlistRepository.CreateAsync(new OrderList { OrderId = newList.OrderId, ProductId = newList.ProductId});
            await _orderlistRepository.SaveAsync();
        }

        public async Task Delete(Guid order, Guid product)
        {
            var orderToDelete = _orderlistRepository.FindByIds(order, product);
            _orderlistRepository.Delete(orderToDelete);
            await _orderlistRepository.SaveAsync();
        }
    }
}
