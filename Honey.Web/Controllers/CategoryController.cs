using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryIndex()
        {
            List<CategoryDto> listCategories = new();

            var productResponse = await _categoryService.GetAllCategoriessAsync<ResponseDto>();

            if (productResponse != null && productResponse.IsSuccess)
            {
                listCategories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(productResponse.Result));
            }

            return View(listCategories);
        }

        public async Task<IActionResult> CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(CategoryDto categoryRequest)
        {
            if(ModelState.IsValid)
            {
                var categoryResponse = await _categoryService.CreateCategoryAsync<ResponseDto>(categoryRequest);

                if (categoryResponse != null && categoryResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(CategoryIndex));
                }
            }    

            return View(categoryRequest);
        }

        public async Task<IActionResult> CategoryEdit(int categoryId)
        {
            CategoryDto categoryInDb = new();

            var categoryResponse = await _categoryService.GetCategoryByIdAsync<ResponseDto>(categoryId);

            if (categoryResponse != null && categoryResponse.IsSuccess)
            {
                categoryInDb = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(categoryResponse.Result));
            }

            return View(categoryInDb);

        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(CategoryDto categoryRequest)
        {
            var categoryResponse = await _categoryService.UpdateCategoryAsync<ResponseDto>(categoryRequest);

            if (categoryResponse != null && categoryResponse.IsSuccess)
            {
                return RedirectToAction(nameof(CategoryIndex));
            }

            return View(categoryRequest);

        }

        public async Task<IActionResult> CategoryDelete(int categoryId)
        {
            CategoryDto categoryInDb = new();

            var categoryResponse = await _categoryService.GetCategoryByIdAsync<ResponseDto>(categoryId);

            if (categoryResponse != null && categoryResponse.IsSuccess)
            {
                categoryInDb = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(categoryResponse.Result));
            }

            return View(categoryInDb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(CategoryDto categoryRequest)
        {
            var categoryResponse = await _categoryService.DeleteCategoryAsync<ResponseDto>(categoryRequest.CategoryId);

            if (categoryResponse.IsSuccess)
            {
                return RedirectToAction(nameof(CategoryIndex));
            }

            return View(categoryRequest);

        }

    }
}
