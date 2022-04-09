using Honey.Services.ShoppingCartAPI.Model.Dto;
using System.Threading.Tasks;

namespace Honey.Services.ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartById(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartRequest);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
        Task<bool> MinusPlusCart(int cartDetailId, int cartHeaderId, bool isPlus);
    }
}
