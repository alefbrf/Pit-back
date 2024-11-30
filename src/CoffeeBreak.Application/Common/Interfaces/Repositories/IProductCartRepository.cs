using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IProductCartRepository : IBaseRepository<ProductCart>
    {
        List<ProductResponse> GetPagedByName(int page, int limit, string name, int userId);
        int GetTotalCount(int userId);
        ProductCart? GetByUserByProduct(int userId, int productId);
        void RemoveProductsByUser(List<int> productsId, int userId);
    }
}
