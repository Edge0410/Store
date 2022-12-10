
using Store.Services.Users;
using Store.Helpers.JwtUtils;
using Store.Repositories.UsersRepository;

namespace Store.Helpers.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient<IDatabaseRepository, DatabaseRepository>();
            services.AddTransient<IUserRepository, UsersRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();

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
