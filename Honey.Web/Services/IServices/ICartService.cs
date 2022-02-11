using Honey.Web.Models.Dto;
using System.Threading.Tasks;

namespace Honey.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartAsync<T>(CartDto cartRequest, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartRequest, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cartId, string token = null);
    }
}
