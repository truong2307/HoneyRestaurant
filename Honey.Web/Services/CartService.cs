using Honey.Web.Models;
using Honey.Web.Models.Dto;
using Honey.Web.Services.IServices;
using System.Net.Http;
using System.Threading.Tasks;

namespace Honey.Web.Services
{
    public class CartService : BaseService,ICartService
    {
        public CartService(IHttpClientFactory httpClient) : base(httpClient)
        { }

        public async Task<T> AddToCartAsync<T>(CartDto cartRequest, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartRequest,
                Url = SD.ShoppingCartAPIBase + "api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "api/cart/GetCart/" + userId,
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartId,
                Url = SD.ShoppingCartAPIBase + "api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartRequest, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartRequest,
                Url = SD.ShoppingCartAPIBase + "api/cart/UpdateCart",
                AccessToken = token
            });
        }

        public async Task<T> MinusPlusCart<T>(int cartDetailId, int cartHeaderId, bool isPlus)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "api/cart/PlusCart?cartDetailId=" + cartDetailId + "&cartHeaderId="+ cartHeaderId + "&isPlus=" +isPlus,
            });
        }
    }
}
