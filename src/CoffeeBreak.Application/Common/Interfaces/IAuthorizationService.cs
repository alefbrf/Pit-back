namespace CoffeeBreak.Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        Dictionary<string, string> DecodeToken(string token);
    }
}
