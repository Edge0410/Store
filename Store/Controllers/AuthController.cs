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
            var userToCreate = new User
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = Roles.User,
                Email = user.Email,
                PasswordHash = BCryptNet.HashPassword(user.Password)
            };

            await _userService.Create(userToCreate);
            return Ok();
        }
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(UserRequestDto user)
        {
            var userToCreate = new User
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = Roles.Admin,
                Email = user.Email,
                PasswordHash = BCryptNet.HashPassword(user.Password)
            };

            await _userService.Create(userToCreate);
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

        [HttpGet("see-admin"), Authorize(Roles = "Admin")]
        public IActionResult GetAllAdmin()
        {
            //var users = _userService.GetAllUsers();
            return Ok("Admin");
        }

        [HttpGet("see-user"), Authorize]
        public IActionResult GetAllUser()
        {
            return Ok("User");
        }

        [HttpDelete("delete-user"), Authorize]
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userService.Delete(username);
            return Ok("User " + username + " was removed");
        }
    }
}
