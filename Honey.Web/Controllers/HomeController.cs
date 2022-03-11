using Honey.Web.Models;
using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> productListInDb = new();
            var responseProduct = await _productService.GetAllProductsAsync<ResponseDto>("");

            if (responseProduct != null && responseProduct.IsSuccess)
            {
                productListInDb = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseProduct.Result));
            }

            CartDto cart = new CartDto();

            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            
            if (!string.IsNullOrEmpty(userId))
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var responseCart = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

                if (responseCart != null && responseCart.IsSuccess)
                {
                    cart = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseCart.Result));
                }

                var amountCart = cart.CartDetails.Count();

                HttpContext.Session.SetInt32(SD.SessionShoppingCart, amountCart);
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

        /// <summary>
        /// Thêm order vào giỏ hàng
        /// </summary>
        /// <param name="productRequest">object Order vào giỏ hàng</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(ProductDto productRequest)
        {
            CartDto cartDto = new CartDto()
            {
                //Lấy id user
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                },
            };

            //Lấy số lượng order và productId
            CartDetailsDto CartDetail = new CartDetailsDto()
            {
                Count = productRequest.Count,
                ProductId = productRequest.ProductId
            };

            //Lấy product theo order để gán vào CartDetail.Product
            var productInDb = await _productService.GetProductByIdAsync<ResponseDto>(productRequest.ProductId, "");

            if (productInDb != null && productInDb.IsSuccess)
            {
                CartDetail.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(productInDb.Result));
                productRequest = CartDetail.Product;
            }


            List<CartDetailsDto> CartDetailList = new List<CartDetailsDto>();
            CartDetailList.Add(CartDetail);
            cartDto.CartDetails = CartDetailList;

            //get token access của user để add cart cho user
            var tokenAccess = await HttpContext.GetTokenAsync("access_token");
            var cartRsp = await _cartService.AddToCartAsync<ResponseDto>(cartDto, tokenAccess);

            if (cartRsp != null && cartRsp.IsSuccess)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    CartDto cart = new CartDto();
                    var accessToken = await HttpContext.GetTokenAsync("access_token");
                    var responseCart = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

                    if (responseCart != null && responseCart.IsSuccess)
                    {
                        cart = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseCart.Result));
                    }

                    var amountCart = cart.CartDetails.Count();

                    HttpContext.Session.SetInt32(SD.SessionShoppingCart, amountCart);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(productRequest);
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
            HttpContext.Session.SetInt32(SD.SessionShoppingCart, 0);

            return SignOut("Cookies","oidc");
        }
    }
}
