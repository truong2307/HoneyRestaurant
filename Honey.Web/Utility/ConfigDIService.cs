using Honey.Web.Services.IServices;
using Honey.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Authentication;

namespace Honey.Web.Utility
{
    public static class ConfigDIService
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            // register http client
            services.AddHttpClient<IProductService, ProductService>();
            services.AddHttpClient<ICategoryService, CategoryService>();
            services.AddHttpClient<ICartService, CartService>();

            // register Multiple service
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IProductService, ProductService>(),
                ServiceDescriptor.Scoped<ICategoryService,CategoryService>(),
                ServiceDescriptor.Scoped<ICartService,CartService>()
            });

            //get value appsetting
            SD.ProductAPIBase = configuration["ServiceUrls:ProductAPI"];
            SD.ShoppingCartAPIBase = configuration["ServiceUrls:ShoppingCartAPI"];

            return services;
        }

        public static IServiceCollection ConfigToUseIdentitySever(this IServiceCollection services, IConfiguration configuration)
        {
            // Sử dụng cookie để làm lượt đồ mặc định, khi user request login sẽ dùng cố gắng dùng cookie của người dùng để xác thực, nên set DefaultScheme = "Cookies" 
            // Trường hợp người dùng muốn chuyển hướng đến provider (identity sever ) để login thì có thể đăng nhập và return về với danh tính, nên set DefaultChallengeScheme = "oidc".
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(15))
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["ServiceUrls:IdentityAPI"];
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClientId = "honey";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.ClaimActions.MapJsonKey("role", "role", "role");
                    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.Scope.Add("honey");
                    options.SaveTokens = true;
                });

            return services;
        }
    }
}
