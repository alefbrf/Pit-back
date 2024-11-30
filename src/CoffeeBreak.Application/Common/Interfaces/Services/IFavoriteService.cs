using CoffeeBreak.Application.DTOs.Response.Product;

namespace CoffeeBreak.Application.Common.Interfaces.Services
{
    public interface IFavoriteService
    {
        List<ProductResponse> GetAll(string jwtToken, int page, int limit, string name);
        int GetTotaCount(string jwtToken);
        ProductResponse Create(string jwtToken, int productId);
        void Delete(string jwtToken, int productId);
    }
}
