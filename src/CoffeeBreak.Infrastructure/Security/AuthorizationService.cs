using CoffeeBreak.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace CoffeeBreak.Infrastructure.Security
{
    public class AuthorizationService : IAuthorizationService
    {
        public Dictionary<string, string> DecodeToken(string token)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var claims = jwtSecurityToken.Claims.ToList();

            var TokenInfo = new Dictionary<string, string>();

            foreach (var claim in claims)
            {
                TokenInfo.Add(claim.Type, claim.Value);
            }

            return TokenInfo;
        }
    }
}
