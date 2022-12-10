using Store.Models;
using Store.Repositories.GenericRepository;

namespace Store.Repositories.UsersRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User FindByUsername(string username);   
    }
}
