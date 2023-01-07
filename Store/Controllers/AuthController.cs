using Store.Models;
using Store.Models.DTOs;
//using Store.Models.Enums;
using Store.Services.Users;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
//using Store.Helpers.Attributes;
using System.Data;
using Store.Models.Enums;
using Store.Helpers.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsersService _userService;

        public AuthController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserRequestDto user)
        {
            var newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = Roles.User,
                Email = user.Email,
                PasswordHash = BCryptNet.HashPassword(user.Password)
            };

            await _userService.Create(newUser);
            return Ok();
        }
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(UserRequestDto user)
        {
            var newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = Roles.Admin,
                Email = user.Email,
                PasswordHash = BCryptNet.HashPassword(user.Password)
            };

            await _userService.Create(newUser);
            return Ok();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginUserRequestDto user)
        {
            var response = _userService.Authentificate(user);
            if (response == null)
            {
                return BadRequest("Username or password is invalid!");
            }
            return Ok(response.Token);
        }

        [HttpGet("show-users"), Authorize(Roles = "Admin")]
        public Task<List<User>> ShowAllUsers()
        {
            var users = _userService.GetAllUsers();
            return users;
        }

        [HttpGet("show-user"), Authorize(Roles = "User")]
        public IActionResult ShowUser()
        {
            return Ok("Logged in as standard User");
        }

        [HttpPut("edit/{id}"), Authorize]
        public async Task<IActionResult> EditUser(Guid id, UserEditDto editUser)
        {
            await _userService.Edit(id, editUser);
            return Ok("User was modified");
        }


        [HttpDelete("delete/{username}"), Authorize]
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userService.Delete(username);
            return Ok("User " + username + " was removed");
        }
    }
}
