using Honey.Web.Models.Dto;
using System.Threading.Tasks;

namespace Honey.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int productId);
        Task<T> CreateProductAsync<T>(ProductDto productRequest);
        Task<T> UpdateProductAsync<T>(ProductDto productRequest);
        Task<T> DeleteProductAsync<T>(int productId);
    }
}
