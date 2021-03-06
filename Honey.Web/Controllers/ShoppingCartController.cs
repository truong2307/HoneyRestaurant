using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Honey.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ICartService _cartService;

        public ShoppingCartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        
        public async Task<IActionResult> Index()
        {
            CartDto cartInDb = new CartDto();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.FirstOrDefault(u => u.Type == "sub").Value;

            var responseCart = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);
            int ammountCart = 0;

            if (responseCart.IsSuccess && responseCart != null)
            {
                cartInDb = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseCart.Result));

                if (cartInDb.CartHeader != null)
                {
                    ammountCart = cartInDb.CartDetails.Count();
                    double totalPrice = 0;

                    foreach (var item in cartInDb.CartDetails)
                    {
                        totalPrice += (double)item.Count * item.Product.Price;
                    }

                    cartInDb.CartHeader.OrderTotal = totalPrice;
                }
               
            }

            HttpContext.Session.SetInt32(SD.SessionShoppingCart, ammountCart);

            return View(cartInDb);
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var responseRemoveCart = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, accessToken);

            if (responseRemoveCart != null && responseRemoveCart.IsSuccess)
            {
                var tryPareResultResponse = bool.TryParse(responseRemoveCart.Result.ToString(), out bool remoteCartSuccess);

                if (remoteCartSuccess && tryPareResultResponse)
                {
                    return Json(new { success = true});
                }
            }

            return PartialView("~/Views/Shared/Error.cshtml");
        }

        public async Task<IActionResult> PlusMinus (int cartDetailsId, bool isPlus)
        {
            CartDto cartInDb = new CartDto();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.FirstOrDefault(u => u.Type == "sub").Value;

            var cartResponse = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);
            if (cartResponse.IsSuccess && cartResponse != null)
            {
                cartInDb = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(cartResponse.Result));

                if (cartInDb.CartHeader != null && cartInDb.CartDetails.Count() > 0)
                {
                    ResponseDto updateCartRsp = new();

                    switch (true)
                    {
                        case true when isPlus:
                             updateCartRsp = await _cartService.MinusPlusCart<ResponseDto>(cartDetailsId, cartInDb.CartHeader.CartHeaderId, isPlus);
                            break;
                        default:
                            updateCartRsp = await _cartService.MinusPlusCart<ResponseDto>(cartDetailsId, cartInDb.CartHeader.CartHeaderId, isPlus);
                            break;
                    }

                    if (updateCartRsp != null && updateCartRsp.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return PartialView("~/Views/Shared/Error.cshtml");
        }
        
    }
}
