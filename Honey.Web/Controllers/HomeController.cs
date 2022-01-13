using Honey.Web.Models;
using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Honey.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> productListInDb = new();
            var responseProduct = await _productService.GetAllProductsAsync<ResponseDto>("");

            if (responseProduct != null && responseProduct.IsSuccess)
            {
                productListInDb = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseProduct.Result));
            }

            return View(productListInDb);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto productInDb = new();
            var responseProduct = await _productService.GetProductByIdAsync<ResponseDto>(productId,"");

            if (responseProduct != null && responseProduct.IsSuccess)
            {
                productInDb = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseProduct.Result));
            }

            return View(productInDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies","oidc");
        }
    }
}
