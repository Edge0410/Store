using Store.Helpers.JwtUtils;
using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.UsersRepository;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Store.Services.Users
{
    public class UsersService: IUsersService
    {
        public IUserRepository _userRepository;
        private IJwtUtils _jwtUtils;

        public UsersService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public UserResponseDto Authentificate(LoginUserRequestDto model)
        {
            var user = _userRepository.FindByUsername(model.Username);
            if(user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                return null; //or throw exception
            }


            // jwt generation
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            return new UserResponseDto(user, jwtToken);
        }

        public async Task Create(User newUser)
        {
            await _userRepository.CreateAsync(newUser);
            await _userRepository.SaveAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public User GetById(Guid id)
        {
            return _userRepository.FindById(id);
        }

        public async Task Edit(Guid id, UserEditDto editUser)
        {
            var foundUser = await _userRepository.FindByIdAsync(id);
            foundUser.Email = editUser.Email;
            foundUser.FirstName = editUser.FirstName;
            foundUser.LastName = editUser.LastName;
            await _userRepository.SaveAsync();
        }

        public async Task Delete(string username)
        {
            var user = _userRepository.FindByUsername(username);
            _userRepository.Delete(user);
            await _userRepository.SaveAsync();
        }
    }
}
