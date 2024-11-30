using Microsoft.AspNetCore.Mvc.Filters;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Consts;

namespace CoffeeBreak.Api.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?)context.HttpContext.Items[ContextKeys.User];
            if (user == null)
            {
                throw new BaseException("Unauthorized", System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
