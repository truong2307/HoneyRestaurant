using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
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
    }
}
