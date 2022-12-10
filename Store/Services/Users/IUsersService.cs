using Store.Models;
using Store.Models.DTOs;

namespace Store.Services.Users
{
    public interface IUsersService
    {
        UserResponseDto Authentificate(UserRequestDto model);
        Task<List<User>> GetAllUsers();
        User GetById(Guid id);
        Task Create(User newUser);
    }
}
