using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Models;
using Store.Models.Enums;
using Store.Models.Enums;
using Store.Models;
using System.Data;

namespace Store.Helpers.Attributes
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ICollection<Roles> _roles;

        public AuthorizationAttribute(params Roles[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusObject = new JsonResult(new { Message = "Unauthorzed" })
            { StatusCode = StatusCodes.Status401Unauthorized };


            if (_roles == null)
            {
                context.Result = unauthorizedStatusObject;
            }

            var user = (User)context.HttpContext.Items["User"];
            if (user == null || !_roles.Contains(user.Role))
            {
                context.Result = unauthorizedStatusObject;
            }
        }
    }
}