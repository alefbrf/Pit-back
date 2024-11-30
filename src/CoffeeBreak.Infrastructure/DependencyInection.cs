using CoffeeBreak.Application.Common.Interfaces;
using CoffeeBreak.Application.Common.Interfaces.Email;
using CoffeeBreak.Application.Common.Interfaces.Repositories;
using CoffeeBreak.Infrastructure.Email;
using CoffeeBreak.Infrastructure.Persistence;
using CoffeeBreak.Infrastructure.Repositories;
using CoffeeBreak.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBreak.Infrastructure
{
    public static class DependencyInection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //var connection = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseNpgsql(connection)
            //);

            var sqlConnection = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(sqlConnection, dbOptions => {
                    dbOptions.EnableRetryOnFailure();
                    dbOptions.CommandTimeout(500);
                })
            );

            services
                .AddAuthentication(configuration)
                .AddAuthorization()
                .AddCors()
                .AddRepositories()
                .AddCryptography(configuration)
                .AddEmail(configuration);

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
                .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            return services;
        }
        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }

        private static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IProductCartRepository, ProductCartRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IConfigsRepository, ConfigsRepository>();

            return services;
        }

        private static IServiceCollection AddCryptography(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICryptographyService, CryptographyService>();

            return services;
        }

        private static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.Section));
            EmailSettings emailSettings = new();
            configuration.Bind(EmailSettings.Section, emailSettings);
            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}
