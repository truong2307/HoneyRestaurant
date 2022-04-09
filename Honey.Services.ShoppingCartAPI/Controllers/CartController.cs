using Honey.Services.ShoppingCartAPI.Model.Dto;
using Honey.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Honey.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        protected readonly ResponseDto _responseDto;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._responseDto = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cartInDb = await _cartRepository.GetCartById(userId);
                _responseDto.Result = cartInDb;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartRequest)
        {
            try
            {
                CartDto cartResponse = await _cartRepository.CreateUpdateCart(cartRequest);
                _responseDto.Result = cartResponse;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartRequest)
        {
            try
            {
                CartDto cartResponse = await _cartRepository.CreateUpdateCart(cartRequest);
                _responseDto.Result = cartResponse;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartDetailId)
        {
            try
            {
                bool cartDeleted = await _cartRepository.RemoveFromCart(cartDetailId);
                _responseDto.Result = cartDeleted;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPost("PlusCart")]
        public async Task<object> PlusCart(int cartDetailId, int cartHeaderId, bool isPlus)
        {
            try
            {
                bool plusCartRsp = await _cartRepository.MinusPlusCart(cartDetailId, cartHeaderId, isPlus);
                _responseDto.Result = plusCartRsp;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }
    }
}
