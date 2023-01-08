using Store.Repositories.OrderListsRepository;
using Store.Repositories.OrdersRepository;
using Store.Repositories.ProductsRepository;
using Store.Repositories.UsersRepository;

namespace Store.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrdersRepository { get; }
        IOrderListRepository OrderListsRepository { get; }
        Task<bool> SaveAsync();
    }
}
