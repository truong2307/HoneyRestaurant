using AutoMapper;
using Honey.Services.ProductAPI.Models;
using Honey.Services.ProductAPI.Models.Dto;

namespace Honey.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<Category, CategoryDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
