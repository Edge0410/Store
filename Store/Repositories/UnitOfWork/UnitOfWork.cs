using Microsoft.Data.SqlClient;
using Store.Repositories.OrderListsRepository;
using Store.Repositories.OrdersRepository;
using Store.Repositories.ProductsRepository;
using Store.Repositories.UsersRepository;

namespace Store.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _context { get; set; } // pentru saveAsync comun in UOW
        public IUserRepository UserRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IOrderRepository OrdersRepository { get; set; }
        public IOrderListRepository OrderListsRepository { get; set; }

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IProductRepository productRepository, IOrderRepository ordersRepository, IOrderListRepository ordersListRepository)
        {
            _context = context;
            UserRepository = userRepository;
            ProductRepository = productRepository;
            OrdersRepository = ordersRepository;
            OrderListsRepository = ordersListRepository;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }
    }
}
