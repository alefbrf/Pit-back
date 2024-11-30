using CoffeeBreak.Infrastructure.Persistence;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Application.DTOs.Response.Product;
using Microsoft.EntityFrameworkCore;
using CoffeeBreak.Application.Common.Interfaces.Repositories;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class ProductCartRepository : BaseRepository<ProductCart>, IProductCartRepository
    {
        private AppDbContext _context;

        public ProductCartRepository(AppDbContext context) : base(context)
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
                from productCart in _context.ProductsCart
                join product in _context.Products on productCart.ProductId equals product.Id
                let favorite = _context.Favorites.Any(e => e.UserId == userId && e.ProductId == product.Id)
                where
                    productCart.UserId == userId &&
                    EF.Functions.Like(product.Name, $"%{name}%")
                select new ProductResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Favorite = favorite
                }
            ).Skip(skip).Take(limit).AsNoTracking().ToList();

            return products;
        }

        public int GetTotalCount(int userId)
        {
            return _context.ProductsCart.Where(e => e.UserId == userId).Count();
        }

        public ProductCart? GetByUserByProduct(int userId, int productId)
        {
            return _context.ProductsCart.FirstOrDefault(Item => Item.UserId == userId && Item.ProductId == productId);
        }

        public void RemoveProductsByUser(List<int> productsId, int userId)
        {
            DeleteWhere(prod => prod.UserId == userId && productsId.Contains(prod.ProductId));
        }
    }
}
