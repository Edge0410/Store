using Store.Helpers.JwtUtils;
using Store.Models;
using Store.Models.DTOs;
using Store.Repositories.UnitOfWork;
using Store.Repositories.UsersRepository;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Store.Services.Users
{
    public class UsersService: IUsersService
    {
        public IUnitOfWork _unitOfWork;
        private IJwtUtils _jwtUtils;

        public UsersService(IUnitOfWork unitOfwork, IJwtUtils jwtUtils)
        {
            _unitOfWork = unitOfwork;
            _jwtUtils = jwtUtils;
        }

        public UserResponseDto Authentificate(LoginUserRequestDto model)
        {
            var user = _unitOfWork.UserRepository.FindByUsername(model.Username);
            if(user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                return null; //or throw exception
            }


            // jwt generation
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken();
            return new UserResponseDto(user, jwtToken, refreshToken);
        }

        public UserResponseDto RefreshTokens(Guid? id)
        {
            var user = _unitOfWork.UserRepository.FindById(id);
            // jwt generation
            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken();
            return new UserResponseDto(user, jwtToken, refreshToken);
        }

        public async Task Create(User newUser)
        {
            await _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public User GetById(Guid id)
        {
            return _unitOfWork.UserRepository.FindById(id);
        }

        public async Task Edit(Guid id, UserEditDto editUser)
        {
            var foundUser = await _unitOfWork.UserRepository.FindByIdAsync(id);
            foundUser.Email = editUser.Email;
            foundUser.FirstName = editUser.FirstName;
            foundUser.LastName = editUser.LastName;
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(string username)
        {
            var user = _unitOfWork.UserRepository.FindByUsername(username);
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task SetRefreshToken(string username, RefreshToken token)
        {
            var user = _unitOfWork.UserRepository.FindByUsername(username);
            user.RefreshToken = token.Token;
            user.RefreshTokenCreationDate = token.RefreshTokenCreationDate;
            user.RefreshTokenExpirationDate = token.RefreshTokenExpirationDate;
            await _unitOfWork.SaveAsync();
        }
        public async Task<RefreshToken> GetRefreshToken(Guid? id)
        {
            var user = _unitOfWork.UserRepository.FindById(id);
            var refreshToken = new RefreshToken
            {
                Token = user.RefreshToken,
                RefreshTokenCreationDate = user.RefreshTokenCreationDate,
                RefreshTokenExpirationDate = user.RefreshTokenExpirationDate
            };
            return refreshToken;
        }
        public async Task<Guid> GetIdFromToken(string token)
        {
            var user = _jwtUtils.ValidateJwtToken(token);
            return user;
        }

    }
}
