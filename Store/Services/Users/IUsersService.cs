using Store.Models;
using Store.Models.DTOs;

namespace Store.Services.Users
{
    public interface IUsersService
    {
        UserResponseDto Authentificate(LoginUserRequestDto model);
        UserResponseDto RefreshTokens(Guid? id);
        Task<List<User>> GetAllUsers();
        User GetById(Guid id);
        Task Create(User newUser);
        Task Edit(Guid id, UserEditDto editUser);
        Task Delete(string username);
        Task SetRefreshToken(string username, RefreshToken token);
        Task<RefreshToken> GetRefreshToken(Guid? id);
        Task<Guid> GetIdFromToken(string token);
    }
}
