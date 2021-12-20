using Honey.Web.Services.IServices;
using Honey.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;

namespace Honey.Web.Utility
{
    public static class ConfigDIService
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            // register http client
            services.AddHttpClient<IProductService, ProductService>();
            services.AddHttpClient<ICategoryService, CategoryService>();

            // register Multiple service
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IProductService, ProductService>(),
                ServiceDescriptor.Scoped<ICategoryService,CategoryService>()
            });

            //get value appsetting
            SD.ProductAPIBase = configuration["ServiceUrls:ProductAPI"];

            return services;
        }
    }
}
