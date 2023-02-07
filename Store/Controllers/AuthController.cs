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
using Azure.Identity;
using Store.Helpers.JwtUtils;
using Azure;

namespace Store.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsersService _userService;
        private IJwtUtils jwtUtils;
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
            var response = _userService.Authentificate(user); // creez jwt token si refresh token
            if (response == null)
            {
                return BadRequest("Username or password is invalid!");
            }
            SetRefreshToken(response.RefreshToken); // bag in cookie
            await _userService.SetRefreshToken(user.Username, response.RefreshToken); // bag in bd refresh token la user 
            return Ok(response.Token);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string oldToken)
        {
            var cookieRefreshToken = Request.Cookies["refreshToken"];
            var user = await _userService.GetIdFromToken(oldToken);
            
            if (user == Guid.Empty)
                return Unauthorized("Token undefined.");
            
            var databaseRefreshToken = await _userService.GetRefreshToken(user);
            
            if(databaseRefreshToken.Token != cookieRefreshToken || databaseRefreshToken.RefreshTokenExpirationDate <= DateTime.Now)
            {
                return Unauthorized("Token expired or altered. Please refresh and try again");
            }

            var newTokens = _userService.RefreshTokens(user);
            SetRefreshToken(newTokens.RefreshToken);
            var userFromId = _userService.GetById(user);
            await _userService.SetRefreshToken(userFromId.Username, newTokens.RefreshToken);

            return Ok(newTokens);
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

        [HttpPut("edit"), Authorize]
        public async Task<IActionResult> EditUser(Guid id, UserEditDto editUser)
        {
            await _userService.Edit(id, editUser);
            return Ok("User was modified");
        }


        [HttpDelete("delete"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userService.Delete(username);
            return Ok("User " + username + " was removed");
        }

        private void SetRefreshToken(RefreshToken newtoken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newtoken.RefreshTokenExpirationDate
            };
            Response.Cookies.Append("refreshToken", newtoken.Token, cookieOptions);
        }
    }
}
