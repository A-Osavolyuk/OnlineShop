using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Interfaces;
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
    }
}
