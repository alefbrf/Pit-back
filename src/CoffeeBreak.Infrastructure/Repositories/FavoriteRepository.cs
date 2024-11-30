using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        private AppDbContext _context;

        public FavoriteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductResponse> GetPagedByName(int page, int limit, string name, int userId)
        {
            if (page == 0)
            {
                page = 1;
            }

            if (limit == 0)
            {
                limit = 30;
            }

            var skip = (page - 1) * limit;

            var products = (
                from favorite in _context.Favorites
                join product in _context.Products on favorite.ProductId equals product.Id
                where
                    favorite.UserId == userId &&
                    EF.Functions.Like(product.Name, $"%{name}%")
                select new ProductResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Favorite = true
                }
            ).Skip(skip).Take(limit).AsNoTracking().ToList();

            return products;
        }

        public int GetTotalCount(int userId)
        {
            return _context.Favorites.Where(e => e.UserId == userId).Count();
        }
    }
}
