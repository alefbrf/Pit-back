using CoffeeBreak.Application.Common.Interfaces.Services;
using CoffeeBreak.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBreak.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IProductCartService, ProductCartService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IConfigsService, ConfigsService>();

            return services;
        }
    }
}
