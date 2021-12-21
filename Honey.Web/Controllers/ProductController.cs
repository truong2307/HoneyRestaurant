using Honey.Web.Models.Dto;
using Honey.Web.Models.ViewModel;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honey.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> listProductsInDb = new();

            var productResponse = await _productService.GetAllProductsAsync<ResponseDto>();
            if (productResponse != null && productResponse.IsSuccess)
            {
                listProductsInDb = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(productResponse.Result));
            }

            return View(listProductsInDb);
        }

        public async Task<IActionResult> ProductCreate()
        {
            List<CategoryDto> listCategoriesInDb = null;
            var responseCategory = await _categoryService.GetAllCategoriessAsync<ResponseDto>();

            if (responseCategory != null && responseCategory.IsSuccess)
            {
                listCategoriesInDb = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(responseCategory.Result));
            }
            var productVM = new ProductVM()
            {
                CategoryList = listCategoriesInDb.Select(i => new SelectListItem()
                {
                    Value = i.CategoryId.ToString(),
                    Text = i.Name,
                })
            };

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductVM productRequest)
        {
            if (ModelState.IsValid)
            {
                var productResponse = await _productService.CreateProductAsync<ResponseDto>(productRequest.Product);
                if (productResponse != null && productResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            List<CategoryDto> listCategoriesInDb = null;
            var responseCategory = await _categoryService.GetAllCategoriessAsync<ResponseDto>();

            if (responseCategory != null && responseCategory.IsSuccess)
            {
                listCategoriesInDb = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(responseCategory.Result));
            }
            var productVM = new ProductVM()
            {
                CategoryList = listCategoriesInDb.Select(i => new SelectListItem()
                {
                    Value = i.CategoryId.ToString(),
                    Text = i.Name,
                })
            };

            return View(productVM);
        }

    }
}
