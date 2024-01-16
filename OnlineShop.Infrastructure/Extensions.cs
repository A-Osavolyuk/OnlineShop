using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Common;
using OnlineShop.Infrastructure.Services;
using OnlineShop.Infrastructure.Validation;

namespace OnlineShop.Infrastructure
{
    public static class Extensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IProductService ,ProductService>();
        }

        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IProductService, ProductService>();
        }

        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<HttpData>(configuration.GetSection("HttpData"));
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();
        }
    }
}
