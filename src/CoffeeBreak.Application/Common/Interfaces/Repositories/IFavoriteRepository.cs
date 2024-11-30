using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Repositories
{
    public interface IFavoriteRepository : IBaseRepository<Favorite>
    {
        List<ProductResponse> GetPagedByName(int page, int limit, string name, int userId);
        int GetTotalCount(int userId);
    }
}
