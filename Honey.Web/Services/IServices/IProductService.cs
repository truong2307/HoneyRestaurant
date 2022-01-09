using Honey.Web.Models.Dto;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Honey.Web.Services.IServices
{
    public interface IProductService : IBaseService
{
        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int productId, string token);
        Task<T> CreateProductAsync<T>(ProductDto productRequest, string token);
        Task<T> UpdateProductAsync<T>(ProductDto productRequest, string token);
        Task<T> DeleteProductAsync<T>(int productId, string token);
    }
}
