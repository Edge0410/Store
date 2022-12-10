using Store.Repositories.UsersRepository;
using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.UsersRepository
{
    public class UsersRepository: GenericRepository<User>, IUserRepository
    {
        public UsersRepository(AppDbContext context): base(context)
        {

        }

        public User FindByUsername(string username)
        {
            return _table.FirstOrDefault(x => x.Username == username);
        }
    }
}
