using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.OrderListsRepository;
using Store.Repositories.UnitOfWork;

namespace Store.Services.OrderLists
{
    public class OrderListsService : IOrderListService
    {
        public IUnitOfWork _unitOfWork;
        
        public OrderListsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(OrderListRequestDto newList)
        {
            await _unitOfWork.OrderListsRepository.CreateAsync(new OrderList { OrderId = newList.OrderId, ProductId = newList.ProductId, Quantity = newList.Quantity});
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Guid order, Guid product)
        {
            var orderToDelete = _unitOfWork.OrderListsRepository.FindByIds(order, product);
            _unitOfWork.OrderListsRepository.Delete(orderToDelete);
            await _unitOfWork.SaveAsync();
        }
    }
}
