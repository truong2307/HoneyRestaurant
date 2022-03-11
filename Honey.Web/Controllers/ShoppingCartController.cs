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
    public class ShoppingCartController : Controller
    {
        private readonly ICartService _cartService;

        public ShoppingCartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            CartDto cartInDb = new CartDto();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.FirstOrDefault(u => u.Type == "sub").Value;

            var responseCart = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

            if (responseCart.IsSuccess && responseCart != null)
            {
                cartInDb = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(responseCart.Result));
            }

            var ammountCart = cartInDb.CartDetails.Count();

            HttpContext.Session.SetInt32(SD.SessionShoppingCart, ammountCart);

            return View(cartInDb);
        }
    }
}
