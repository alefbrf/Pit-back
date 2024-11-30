using CoffeeBreak.Application.Common.Consts;
using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Api.Context
{
    public static class Context
    {
        public static User GetUser(HttpContext context)
        {
            var user = (User?)context.Items[ContextKeys.User];
            if (user == null)
            {
                throw new BaseException("Unauthorized", System.Net.HttpStatusCode.Unauthorized);
            }

            return user;
        }
    }
}
