using Honey.Services.ProductAPI.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Services.ProductAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<CategoryDto> UpdateCategory(CategoryDto categoryRequest);
        Task<CategoryDto> CreateCategory(CategoryDto categoryRequest);
        Task<bool> DeleteCategory(int categoryId);
    }
}
