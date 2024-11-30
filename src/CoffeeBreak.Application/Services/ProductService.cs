using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Interfaces;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Request.Product;
using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthorizationService _authorizationService;
        public ProductService(IProductRepository productRepository, IAuthorizationService authorizationService)
        {
            _productRepository = productRepository;
            _authorizationService = authorizationService;
        }

        public List<ProductResponse> GetAll(string jwtToken, int page, int limit, string name)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            return _productRepository.GetPagedByName(page, limit, name, int.Parse(sUserId));
        }

        public int GetTotalCount()
        {
            return _productRepository.GetTotalCount();
        }

        public ProductResponse Get(string jwtToken, int id)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            var product = _productRepository.GetProductById(id, int.Parse(sUserId));
            if (product is null)
            {
                throw new BaseException("Produto não encontrado", System.Net.HttpStatusCode.NotFound);
            }

            return product;
        }

        public Product Create(ProductDTO productDTO)
        {
            var product = new Product();
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.ImageUrl = productDTO.ImageUrl;

            _productRepository.Insert(product, true);
            return product;
        }

        public Product Update(int id, ProductDTO productDTO)
        {
            var product = _productRepository.GetById(id);
            if (product == null) 
            {
                throw new BaseException("Produto não encontrado", System.Net.HttpStatusCode.NotFound);
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.ImageUrl = productDTO.ImageUrl;
            _productRepository.Commit();
            return product;
        }

        public void Delete(int id)
        {
            try
            {
                _productRepository.DeleteWhere(product => product.Id == id);
            }
            catch (Exception)
            {
                throw new BaseException("Tentativa de apagar o produto falhou, recarregue a página e tente novamente.", System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
