using Honey.Services.ProductAPI.Models.Dto;
using Honey.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Services.ProductAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitofwork;
        protected ResponseDto _response;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> GetCategories()
        {
            try
            {
                IEnumerable<CategoryDto> categoriesInDb = await _unitofwork.Category.GetCategories();
                _response.Result = categoriesInDb;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("{categoryId}")]
        public async Task<object> GetCategories(int categoryId)
        {
            try
            {
                CategoryDto categoryInDb = await _unitofwork.Category.GetCategoryById(categoryId);
                _response.Result = categoryInDb;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        public async Task<object> CreateCategory(CategoryDto categoryRequest)
        {
            try
            {
                CategoryDto categoryInDb = await _unitofwork.Category.CreateCategory(categoryRequest);
                _response.Result = categoryInDb;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut]
        public async Task<object> UpdateCatgory(CategoryDto categoryRequest)
        {
            try
            {
                CategoryDto categoryInDb = await _unitofwork.Category.UpdateCategory(categoryRequest);
                _response.Result = categoryInDb;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete("{categoryId}")]
        public async Task<object> DeleteCategory(int categoryId)
        {
            try
            {
                bool categoryDeleted = await _unitofwork.Category.DeleteCategory(categoryId);
                _response.Result = categoryDeleted;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

    }
}
