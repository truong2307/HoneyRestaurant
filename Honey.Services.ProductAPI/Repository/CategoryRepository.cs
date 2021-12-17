using AutoMapper;
using Honey.Services.ProductAPI.DbContexts;
using Honey.Services.ProductAPI.Models;
using Honey.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Services.ProductAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CategoryDto> UpdateCategory(CategoryDto categoryRequest)
        {
            Category categoryInDb = await _db.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryRequest.CategoryId);

            categoryInDb.Name = categoryRequest.Name;
            await _db.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(categoryInDb);
        }

        public async Task<CategoryDto> CreateCategory(CategoryDto categoryRequest)
        {
            Category categoryToDb = _mapper.Map<Category>(categoryRequest);
            _db.Categories.Add(categoryToDb);
            await _db.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(categoryToDb);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                Category categoryInDb = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
                if (categoryInDb == null)
                {
                    return false;
                }
                _db.Categories.Remove(categoryInDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            List<Category> categoriesInDb = await _db.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categoriesInDb);

        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            Category categoryInDb = await _db.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            return _mapper.Map<CategoryDto>(categoryInDb);
        }
    }
}
