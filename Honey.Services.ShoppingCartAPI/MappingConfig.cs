using AutoMapper;

namespace Honey.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<Product, ProductDto>().ReverseMap();
                //config.CreateMap<Category, CategoryDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
