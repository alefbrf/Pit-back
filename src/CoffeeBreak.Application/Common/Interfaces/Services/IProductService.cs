using CoffeeBreak.Application.DTOs.Request.Product;
using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Common.Interfaces.Services
{
    public interface IProductService
    {
        List<ProductResponse> GetAll(string jwtToken, int page, int limit, string name);
        ProductResponse Get(string jwtToken, int id);
        Product Create(ProductDTO productDTO);
        Product Update(int id, ProductDTO productDTO);
        void Delete(int id);
        int GetTotalCount();
    }
}
