using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Interfaces;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Response.Product;

namespace CoffeeBreak.Application.Services
{
    public class ProductCartService : IProductCartService
    {
        private readonly IProductCartRepository _productCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAuthorizationService _authorizationService;

        public ProductCartService(IProductCartRepository productCartRepository, IProductRepository productRepository, IAuthorizationService authorizationService)
        {
            _productCartRepository = productCartRepository;
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

            return _productCartRepository.GetPagedByName(page, limit, name, int.Parse(sUserId));
        }
        public int GetTotaCount(string jwtToken)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            return _productCartRepository.GetTotalCount(int.Parse(sUserId));
        }

        public ProductResponse Create(string jwtToken, int productId)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            var userId = int.Parse(sUserId);

            var product = _productRepository.GetProductById(productId, userId);
            if (product is null)
            {
                throw new BaseException("Produto não encontrado", System.Net.HttpStatusCode.NotFound);
            }

            var productCart = _productCartRepository.GetByUserByProduct(userId, productId);
            if (productCart is null)
            {
                productCart = new()
                {
                    ProductId = productId,
                    UserId = userId
                };

                _productCartRepository.Insert(productCart, true);
            }

            return product;
        }

        public void Delete(string jwtToken, int productId)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            try
            {
                _productCartRepository.DeleteWhere(e => e.UserId == int.Parse(sUserId) && e.ProductId == productId);
            }
            catch (Exception)
            {
                throw new BaseException("Tentativa de apagar o favorito falhou, recarregue a página e tente novamente.", System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
