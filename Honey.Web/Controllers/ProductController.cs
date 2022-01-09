using Honey.Web.Models.Dto;
using Honey.Web.Models.ViewModel;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var productResponse = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);
            if (productResponse != null && productResponse.IsSuccess)
            {
                listProductsInDb = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(productResponse.Result));
            }

            return View(listProductsInDb);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductCreate()
        {
            List<CategoryDto> listCategoriesInDb = null;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var responseCategory = await _categoryService.GetAllCategoriessAsync<ResponseDto>(accessToken);

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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductVM productRequest)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                
                var productResponse = await _productService.CreateProductAsync<ResponseDto>(productRequest.Product, accessToken);
                if (productResponse != null && productResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            List<CategoryDto> listCategoriesInDb = null;
            var responseCategory = await _categoryService.GetAllCategoriessAsync<ResponseDto>(accessToken);

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ProductDto productInDb = new();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var productResponse = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (productResponse != null && productResponse.IsSuccess)
            {
                productInDb = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(productResponse.Result));
            }

            List<CategoryDto> listCategoriesInDb = null;

            var responseCategory = await _categoryService.GetAllCategoriessAsync<ResponseDto>(accessToken);
            if (responseCategory != null && responseCategory.IsSuccess)
            {
                listCategoriesInDb = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(responseCategory.Result));
            }

            var productVM = new ProductVM()
            {
                Product = productInDb,
                CategoryList = listCategoriesInDb.Select(i => new SelectListItem()
                {
                    Value = i.CategoryId.ToString(),
                    Text = i.Name,
                })
            };

            return View(productVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductVM productRequest)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var productResponse = await _productService.UpdateProductAsync<ResponseDto>(productRequest.Product, accessToken);
                if (productResponse != null && productResponse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            List<CategoryDto> listCategoriesInDb = null;
            var responseCategory = await _categoryService.GetAllCategoriessAsync<ResponseDto>(accessToken);

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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ProductDto productInDb = new();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var productResponse = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (productResponse.IsSuccess)
            {
                productInDb = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(productResponse.Result));
            }

            return View(productInDb);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto productRequest)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var productResponse = await _productService.DeleteProductAsync<ResponseDto>(productRequest.ProductId, accessToken);
            if (productResponse.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(productRequest);
        }
    }
}
