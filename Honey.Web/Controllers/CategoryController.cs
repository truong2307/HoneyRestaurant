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

        
    }
}
