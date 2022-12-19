
using Store.Services.Users;
using Store.Services.Products;
using Store.Helpers.JwtUtils;
using Store.Repositories.UsersRepository;
using Store.Repositories.ProductsRepository;
using Store.Repositories.OrdersRepository;
using Store.Services.Orders;
using Store.Repositories.OrderListsRepository;
using Store.Services.OrderLists;

namespace Store.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<IDatabaseRepository, DatabaseRepository>();
            services.AddTransient<IUserRepository, UsersRepository>();
            services.AddTransient<IProductRepository, ProductsRepository>();
            services.AddTransient<IOrderRepository, OrdersRepository>();
            services.AddTransient<IOrderListRepository, OrderListsRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IProductService, ProductsService>();
            services.AddTransient<IOrderService, OrdersService>();
            services.AddTransient<IOrderListService, OrderListsService>();


            return services;
        }

        /*public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<StudentsSeeder>();
            return services;
        }
        */

        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddScoped<IJwtUtils, JwtUtils.JwtUtils>();
            return services;
        }
    }
}
