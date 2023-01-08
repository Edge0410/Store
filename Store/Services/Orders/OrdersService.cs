using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.OrdersRepository;
using Store.Repositories.UnitOfWork;

namespace Store.Services.Orders
{
    public class OrdersService : IOrderService
    {
        public IUnitOfWork _unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Order FindById(Guid id)
        {
            return _unitOfWork.OrdersRepository.FindById(id);
        }

        public OrderWithProductsDto ShowDetails(Guid id)
        {
            return _unitOfWork.OrdersRepository.ShowDetails(id);
        }

        public async Task Create(Order newOrder)
        {
            await _unitOfWork.OrdersRepository.CreateAsync(newOrder);
            await _unitOfWork.SaveAsync();
        }

        public async Task Edit(Guid id, OrderRequestDto editOrder)
        {
            var orderFound = await _unitOfWork.OrdersRepository.FindByIdAsync(id);
            orderFound.Description = editOrder.Description;
            orderFound.DeliveryAddress = editOrder.DeliveryAddress;
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var order = _unitOfWork.OrdersRepository.FindById(id);
            _unitOfWork.OrdersRepository.Delete(order);
            await _unitOfWork.SaveAsync();
        }

    }
}
