using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<ProductResponse> GetPagedByName(int page, int limit, string name, int userId);
        int GetTotalCount();
        ProductResponse? GetProductById(int id, int userId);
        List<ProductResponse> GetProductsById(List<int> productsId);
    }
}
