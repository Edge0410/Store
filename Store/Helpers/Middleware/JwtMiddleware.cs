using Store.Helpers.JwtUtils;
using Store.Services.Users;

namespace Store.Helpers.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public JwtMiddleware(RequestDelegate requestDelegate)
        {
            _nextRequestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext, IUsersService usersService, IJwtUtils jwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();

            var userId = jwtUtils.ValidateJwtToken(token);

            if(userId != Guid.Empty)
            {
                httpContext.Items["User"] = usersService.GetById(userId);
            }

            await _nextRequestDelegate(httpContext);
        }

    }
}
