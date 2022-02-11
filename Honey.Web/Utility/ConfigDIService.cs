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

            // register Multiple service
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IProductService, ProductService>(),
                ServiceDescriptor.Scoped<ICategoryService,CategoryService>()
            });

            //get value appsetting
            SD.ProductAPIBase = configuration["ServiceUrls:ProductAPI"];
            SD.ShoppingCartAPIBase = configuration["ServiceUrls:ShoppingCartAPI"];

            return services;
        }

        public static IServiceCollection ConfigToUseIdentitySever(this IServiceCollection services, IConfiguration configuration)
        {
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
