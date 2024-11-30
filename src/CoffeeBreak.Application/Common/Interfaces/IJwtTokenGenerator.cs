using CoffeeBreak.Application.Common.Enums;

namespace CoffeeBreak.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(
            int id,
            string name,
            string email,
            Role role
        );
    }
}
