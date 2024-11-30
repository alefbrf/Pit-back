using CoffeeBreak.Application.Common.Exceptions;
using CoffeeBreak.Application.Common.Interfaces;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.DTOs.Response.Product;
using CoffeeBreak.Domain.Entities;

namespace CoffeeBreak.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProductRepository _productRepository;
        public FavoriteService(IFavoriteRepository favoriteRepository, IAuthorizationService authorizationService, IProductRepository productRepository)
        {
            _favoriteRepository = favoriteRepository;
            _authorizationService = authorizationService;
            _productRepository = productRepository;
        }

        public List<ProductResponse> GetAll(string jwtToken, int page, int limit, string name)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            return _favoriteRepository.GetPagedByName(page, limit, name, int.Parse(sUserId));
        }

        public int GetTotaCount(string jwtToken)
        {
            var decodedToken = _authorizationService.DecodeToken(jwtToken);

            string sUserId;

            if (!decodedToken.TryGetValue("id", out sUserId))
            {
                throw new BaseException($"Invalid token: {jwtToken}", System.Net.HttpStatusCode.BadRequest);
            };

            return _favoriteRepository.GetTotalCount(int.Parse(sUserId));
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

            Favorite favorite = new()
            {
                ProductId = productId,
                UserId = userId
            };

            _favoriteRepository.Insert(favorite, true);

            product.Favorite = true;

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
                _favoriteRepository.DeleteWhere(e => e.UserId == int.Parse(sUserId) && e.ProductId == productId);
            }
            catch (Exception)
            {
                throw new BaseException("Tentativa de apagar o favorito falhou, recarregue a página e tente novamente.", System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
