using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Common;
using OnlineShop.Infrastructure.Services;

namespace OnlineShop.Infrastructure
{
    public static class Extensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IBaseService, BaseService>();
        }

        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();
        }

        public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<HttpData>(configuration.GetSection("HttpData"));
        }
    }
}
