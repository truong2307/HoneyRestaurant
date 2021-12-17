using Honey.Services.ProductAPI.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productRequest);
        Task<bool> DeleteProduct(int productId);
    }
}
