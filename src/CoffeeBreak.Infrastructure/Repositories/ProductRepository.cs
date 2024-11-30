using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;
using CoffeeBreak.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBreak.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
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
                from product in _context.Products
                let favorite = _context.Favorites.Any(e => e.UserId == userId && e.ProductId == product.Id)
                where
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

        public ProductResponse? GetProductById(int id, int userId) 
        {
           var productEntity = (
                from product in _context.Products
                let favorite = _context.Favorites.Any(e => e.UserId == userId && e.ProductId == product.Id)
                where
                    product.Id == id
                select new ProductResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Favorite = favorite,
                }
            ).FirstOrDefault();

            return productEntity;
        }

        public int GetTotalCount()
        {
            return _context.Products.Count();
        }

        public List<ProductResponse> GetProductsById(List<int> productsId)
        {
            return _context.Products.Where(p => productsId.Contains(p.Id)).
                Select(p => new ProductResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).AsNoTracking().ToList();
        }
    }
}
