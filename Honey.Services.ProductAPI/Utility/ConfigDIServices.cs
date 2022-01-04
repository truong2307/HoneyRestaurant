using AutoMapper;
using Honey.Services.ProductAPI.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Honey.Services.ProductAPI.Utility
{
    public static class ConfigDIServices
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            //config auto mapper
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
