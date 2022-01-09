using Honey.Web.Models.Dto;
using System.Threading.Tasks;

namespace Honey.Web.Services.IServices
{
    public interface ICategoryService : IBaseService
    {
        Task<T> GetAllCategoriessAsync<T>(string token);
        Task<T> GetCategoryByIdAsync<T>(int categoryId, string token);
        Task<T> CreateCategoryAsync<T>(CategoryDto categoryRequest, string token);
        Task<T> UpdateCategoryAsync<T>(CategoryDto categoryRequest, string token);
        Task<T> DeleteCategoryAsync<T>(int categoryId, string token);
    }
}
