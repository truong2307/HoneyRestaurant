using Honey.Web.Models.Dto;
using System.Threading.Tasks;

namespace Honey.Web.Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetAllCategoriessAsync<T>();
        Task<T> GetCategoryByIdAsync<T>(int categoryId);
        Task<T> CreateCategoryAsync<T>(CategoryDto categoryRequest);
        Task<T> UpdateCategoryAsync<T>(CategoryDto categoryRequest);
        Task<T> DeleteCategoryAsync<T>(int categoryId);
    }
}
